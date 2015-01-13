namespace NJekyll.Engine
{
    public class Layout : ContentFile
    {
        internal string Name { get; private set; }
        internal string ParentLayout { get; private set; }

        public Layout(string virtualPath)
            : base(virtualPath)
        {
        }

        protected override void Init()
        {
            // ge layout name from file name
            Name = System.IO.Path.GetFileNameWithoutExtension(this.Path);

            // extract parent layout from front matter
            if (FrontMatter.ContainsKey("layout"))
            {
                ParentLayout = (string)FrontMatter["layout"];
            }
        }
    }
}