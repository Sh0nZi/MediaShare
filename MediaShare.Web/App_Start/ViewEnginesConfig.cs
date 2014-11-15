namespace MediaShare.Web.App_Start
{
    using System.Web.Mvc;

    public static class ViewEnginesConfig
    {
        public static void RegisterEngines()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}