---
title: 2FA comes to AppVeyor
---

Two factor authentication (2FA) is a must for modern enterprise security, and AppVeyor has accordingly implemented it both for enterprise and hosted users. Here is what you need to know to take advantage of the increased protection of your valuable resources associated with your AppVeyor account. 

2FA can be _enabled_ for a user/login and can be _required_ by an account.

## Requiring 2FA for an account

On an account security page as seen below the option to add 2FA to the account can be seen. But note the clarification below the checkbox.

If a source control service (GitHub, Bitbucket, etc.) is added to the allowed authentication methods then the 2FA check can be bypassed if it is not demanded by that service. Therefore, if securing the account with 2FA is the intention and the 3rd party services can not be set up to require it, the best configuration is as seen below, where only the `Email/password` option is allowed for logging into the account.

<p><img src="/assets/img/posts/2FA-implementation/2FA-account-config.png" alt="2FA config"></p>

If a user who has not enabled 2FA and has signed in with email/password attempts to access any resources on an account that requires 2FA they will be redirected to the 2FA setup page seen below. 

<p><img src="/assets/img/posts/2FA-implementation/2FA-setup.png" alt="2FA setup"></p> 

To reiterate, if the user has authenticated with a 3rd party, the AppVeyor account resources will still be accessible to that user. 

Once 2FA is enabled for a user/login the login page will prompt users for an authentication code after entering email and password. 

<p><img src="/assets/img/posts/2FA-implementation/2FA-login.png" alt="2FA login"></p> 
 
## Enabling 2FA for user/login

Even if the accounts you access do not require it, the extra layer of security can give users peace of mind. To opt in navigate to the _My profile -> security_ tab and click the `Enable 2FA` button as seen below. 

<p><img src="/assets/img/posts/2FA-implementation/2FA-enable.png" alt="2FA enable"></p>

The user will then be redirected to the setup page mentioned earlier. Don't forget to store your recovery codes somewhere secure (theories ont the best way to do this are as plentiful as they are contentious). These are a single use bypass of 2FA that will allow you to access your account should your authenticating device no longer be available.




Best regards,<br>
AppVeyor team
