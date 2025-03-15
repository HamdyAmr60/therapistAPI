namespace therapist.API.Helpers
{
    public static class WorkWithImages
    {
        public static string UploadImages(IFormFile file, string FolderName)
        {
            string Folder = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\images" ,FolderName);
            string fileName = Guid.NewGuid() + file.FileName;

            string filePath = Path.Combine(Folder, fileName);

            var fs = new FileStream(filePath , FileMode.Create);
            file.CopyTo(fs);
            return Path.Combine($"wwwroot\\{FolderName}" , fileName);
        }
        public static void DeleteFile(string FolderName, string FileName)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", FolderName);

            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}
