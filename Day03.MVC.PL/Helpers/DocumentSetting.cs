using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Day03.MVC.PL.Helpers
{
	public static class DocumentSetting
	{
		public static string UploadFile(IFormFile file,string FolderName)
		{
			//1.Get Located folder path
			//string FolderPath = $"C:\\rout\\MVC\\Session03\\Pdf\\MVC.Solution\\Day03.MVC.PL\\wwwroot\\Files\\{FolderName}";
			//string FolderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{{FolderName}}"; 
			string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);  //Recommended

			if(!Directory.Exists(folderPath)) 
				Directory.CreateDirectory(folderPath);

			//2.Get File Name and Make it UNIQUE

			string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

			//3.File Path => folder Path + File Name	
			string filePath=Path.Combine(folderPath, fileName);

			//4.Save Files as Streams [Data Per Time]
			var fileStream=new FileStream(filePath,FileMode.Create); 
			file.CopyTo(fileStream);
			return fileName;
			
		}
		public static void Deletefile(string fileName,string FolderName)
		{
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName, fileName);

			if(File.Exists(filePath))
				File.Delete(filePath); 
		}

	}
}
