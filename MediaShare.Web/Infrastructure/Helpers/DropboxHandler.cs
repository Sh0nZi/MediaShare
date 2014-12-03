namespace MediaShare.Web.Infrastructure.Helpers
{
    using System;

    using DropNet;

    public static class DropboxHandler
    {
        private const string ApiKey = "x8nkofu5kb13i41";

        private const string ApiSecret = "f0aitgfpaa3k9zv";

        private const string ApiToken = "giEFVG1vvygAAAAAAAAAInOApbO6gRshlS9nTgHCoQ1b6MvR8W9GC6mgXwaoLtvr";

        public static void UploadFile(byte[] content, string name)
        {
            var client = new DropNetClient(ApiKey, ApiSecret, ApiToken);

            client.UseSandbox = true;
            client.UploadFile("/", name, content);
        }

        public static string GetUrl(string name)
        {
            var client = new DropNetClient(ApiKey, ApiSecret,ApiToken);
            client.UseSandbox = true;
            var medialink = client.GetMedia(name).Url;
            
            return medialink.Replace("dropboxusercontent.com/1/view/", "dropbox.com/s/").Replace("Apps/MediaSharing/", "");
        }
    }
}