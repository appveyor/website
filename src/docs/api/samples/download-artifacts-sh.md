---
title: Downloading AppVeyor build artifacts (ShellScript)
layout: docs
---

# Downloading AppVeyor build artifacts (ShellScript - advanced example)

```sh
./get_appveyor_artifacts.sh --account=ACCOUNT --project=PROJECT --flat --path $TMPDIR --token=TOKEN   # --debug
```

---

```sh
#!/bin/bash

function get_appveyor_artifacts() {
    # Function Parameters
    local account project path="." token branch build commit job_name file_name flat=0 debug=0 proxy api_url="https://ci.appveyor.com/api"

    function help() {
        local me=$0
        [[ "${BASH_SOURCE[0]}" != "${0}" ]] && me="get_appveyor_artifacts"
        echo "Usage: $me [OPTIONS]"
        echo ""
        echo "Options:"
        echo "  --account=VALUE          The name of the account to download artifacts from (required)"
        echo "  --project=VALUE          The name of the project to download artifacts from (required)"
        echo "  --path=VALUE             Where to save the downloaded artifacts (defaults to current directory)"
        echo "  --token=VALUE            Your AppVeyor API token (only needed for non-public projects)"
        echo "  --branch=VALUE           Specify a branch or project directory (e.g., 'master')"
        echo "  --build=VALUE            Specify a build version"
        echo "  --jobname=VALUE          Specify which job to retrieve artifacts from"
        echo "  --commit=VALUE           Specify which commitId to retrieve artifacts from"
        echo "  --filename=VALUE         Filter to a specific artifact filename"
        echo "  --flat                   Download all files into a single directory, without preserving hierarchy"
        echo "  --debug                  Enable debug output"
        echo "  --proxy=VALUE            Proxy server address"
        echo "  --api-url=VALUE          URL of Appveyor API (default is https://ci.appveyor.com/api)"
        echo "  -h, --help               Display this help message"
        echo ""
        echo "Example:"
        echo "  $me --account=myAccount --project=myProject --token=myToken"
    }

    # Check for no parameters and call help function
    if [[ $# -eq 0 ]]; then
        help
        return 1
    fi

    # Nested function to fetch data from the API
    fetch_from_api() {
        local url="$1" desc=$2 response message

        echo "(${FUNCNAME[0]}): fetching $desc..." 1>&2

        response=$(curl "${curl_opts[@]}" "${headers[@]}" -s "$url")
        if [[ -z "$response" ]]; then
            echo "(${FUNCNAME[0]}): Error: Failed to fetch $desc." 1>&2
            printf "(${FUNCNAME[0]}): Response of the following curl command was empty:\ncurl %s %s -s \"%s\"\n" "${curl_opts[*]}" "${headers[*]}" "$url" 1>&2
            return 1
        fi

        [[ "$debug" -eq 1 ]] && printf "DBG (${FUNCNAME[0]}): curl command used:\nDBG (${FUNCNAME[0]}): curl %s %s -s \"%s\"\n" "${curl_opts[*]}" "${headers[*]}" "$url" 1>&2

        # Extract and output message, if any
        message=$(echo "$response" | jq -r '.message' 2>/dev/null)
        if [[ -n "$message" && "$message" != "null" ]]; then
            echo "(${FUNCNAME[0]}): Note: API request provided the following message: $message" 1>&2
        fi

        # Return the response
        echo "$response"

        [[ "$debug" -eq 1 ]] && printf "\n" 1>&2
        return 0
    }

    # Nested function to dowload artifact
    handle_artifact() {
        local url="$1" desc=$2 response message

        [[ "$debug" -eq 1 ]] && echo "DBG: handling artifact: $artifact" 1>&2

        local_artifact_path=$(echo "$artifact" | jq -r '.fileName')
        artifact_type=$(echo "$artifact" | jq -r '.type')

        [[ "$flat" -eq 1 ]] && local_artifact_path=$(basename "$local_artifact_path")

        if [[ -n "$file_name" && "$file_name" != "$local_artifact_path" ]]; then
           [[ "$debug" -eq 1 ]] && echo "DBG: skip not matching artifact $local_artifact_path..." 1>&2
           return
        fi

        local_artifact_path="$path/$local_artifact_path"

        # Create directory structure if not flat
        mkdir -p "$(dirname "$local_artifact_path")"

        artifact_url="$api_url/buildjobs/$job_id/artifacts/$file_name"
        [[ "$debug" -eq 1 ]] && printf "DBG: Downloading artfact using command:\nDBG: curl %s %s -s \"%s\" --output \"%s\"\n" "${curl_opts[*]}" "${headers[*]}" "$artifact_url" "$local_artifact_path" 1>&2

        curl "${curl_opts[@]}" "${headers[@]}" -s "$artifact_url" --output "$local_artifact_path"

        echo "Downloaded artifact from $artifact_url to $local_artifact_path (Type: $artifact_type)"
        [[ "$debug" -eq 1 ]] && ls -l "$local_artifact_path"
        [[ "$debug" -eq 1 ]] && printf "\n" 1>&2
    }

    # Parse parameters
    while [[ $# -gt 0 ]]; do
      case $1 in
        --account=*) account="${1#*=}"; shift ;;
        --account) account="$2"; shift; shift ;;

        --project=*) project="${1#*=}"; shift ;;
        --project) project="$2"; shift; shift ;;

        --path=*|--download-directory=*) path="${1#*=}"; shift ;;
        --path|--download-directory) path="$2"; shift; shift ;;

        --token=*) token="${1#*=}"; shift ;;
        --token) token="$2"; shift; shift ;;

        --branch=*) branch="${1#*=}"; shift ;;
        --branch) branch="$2"; shift; shift ;;

        --build=*) build="${1#*=}"; shift ;;
        --build) build="$2"; shift; shift ;;

        --commit=*) commit="${1#*=}"; shift ;;
        --commit) commit="$2"; shift; shift ;;

        --jobname=*) job_name="${1#*=}"; shift ;;
        --jobname) job_name="$2"; shift; shift ;;

        --filename=*) file_name="${1#*=}"; shift ;;
        --filename) file_name="$2"; shift; shift ;;

        --flat) flat=1; shift ;;
        --debug) debug=1; shift ;;

        --proxy=*) proxy="${1#*=}"; shift ;;
        --proxy) proxy="$2"; shift; shift ;;

        --api-url=*) api_url="${1#*=}"; shift ;;
        --api-url) api_url="$2"; shift; shift ;;

        -h|--help) help; return 0 ;;

        *) echo "Unknown parameter passed: $1"  1>&2; return 1 ;;
      esac
    done

    if [[ -z "$account" || -z "$project" ]]; then
        echo "Error: Account and Project are required." 1>&2
        return 1
    fi

    headers=("-H" "Content-Type: application/json")
    if [[ -n "$token" ]]; then
        headers+=("-H" "Authorization: Bearer $token")
    fi

    curl_opts=()
    [[ "$debug" -eq 1 ]] && curl_opts+=("--verbose")
    [[ -n "$proxy" ]] && curl_opts+=("--proxy" "$proxy")
    curl_opts+=("--location")

    local MAIN_URI="$api_url/projects/$account/$project"
    local response rc

    if [[ -n "$commit" ]]; then
        project_uri="$MAIN_URI/history?recordsNumber=100"
        if [[ -n "$branch" ]]; then
           project_uri="$project_uri&branch=$branch"
        fi

        response=$(fetch_from_api "$project_uri" "project history") || rc=$?
        if [[ $rc -ne 0 ]]; then
            return "$rc"
        fi

        build=$(echo "$response" | jq -r --arg commit "$commit" '.builds[] | select(.commitId == $commit) | .version')
        if [[ -z "$build" ]]; then
            echo "Error: Unable to find builds for commit $commit." 1>&2
            [[ "$debug" -eq 1 ]] && echo "DBG - Last answer: $response" 1>&2
            return 1
        fi

        if [[ $(echo "$build" | wc -l) -gt 1 ]]; then
            echo "Warning: Multiple builds found for commit $commit. Using the last one." 1>&2
            build=$(echo "$build" | head -n 1)
        fi

    fi

    if [[ -n "$build" ]]; then
        project_uri="$MAIN_URI/build/$build"
    elif [[ -n "$branch" ]]; then
        project_uri="$MAIN_URI/branch/$branch"
    fi

    response=$(fetch_from_api "$project_uri" "project details") || rc=$?
    if [[ $rc -ne 0 ]]; then
        return "$rc"
    fi

    # Extract build jobs
    local job_ids
    job_ids=$(echo "$response" | jq -r '.build.jobs[].jobId' 2>/dev/null)
    if [[ -z "$job_ids" ]]; then
        echo "Error: No jobs found for this project."
        [[ "$debug" -eq 1 ]] && echo "DBG - Last answer: $response" 1>&2
        return 1
    fi

    if [[ $(echo "$job_ids" | wc -l) -gt 1 && -z "$job_name" ]]; then
        echo "Multiple jobs found. Please specify the --jobname parameter." 1>&2
        echo "Jobs found: " 1>&2
        echo "$response" | jq -r '.build.jobs[].name' 1>&2
        return 1
    fi

    if [[ -n "$job_name" ]]; then
        job_id=$(echo "$response" | jq -r --arg job_name "$job_name" '.build.jobs[] | select(.name == $job_name) | .jobId')
        if [[ -z "$job_id" ]]; then
            echo "Error: Unable to find a job named $job_name." 1>&2
            return 1
        fi
    else
        job_id=$(echo "$job_ids" | head -n 1)
    fi

    [[ "$debug" -eq 1 ]] && echo "DBG: job_id to use in Appveyor CI: $job_id" 1>&2

    # Fetch artifacts
    artifacts_response=$(fetch_from_api "$api_url/buildjobs/$job_id/artifacts" "artifacts")
    artifacts=$(echo "$artifacts_response" | jq -c '.[]')

    if [[ -z "$artifacts" ]]; then
       echo "Error: Unable to find artifacts." 1>&2
       [[ "$debug" -eq 1 ]] && printf "DBG: artifact_response:\n%s\nDBG: artifact_response end\n\n" "$artifacts_response" 1>&2
       return 1
    fi


    [[ "$debug" -eq 1 ]] && printf "DBG: artifacts found:\n%s\nDBG: artifacts end\n\n" "$artifacts" 1>&2

    echo "$artifacts" | while IFS= read -r artifact; do
        handle_artifact "$artifact"
    done
}

# function call
[[ "${BASH_SOURCE[0]}" = "${0}" ]] && get_appveyor_artifacts "$@"
```
