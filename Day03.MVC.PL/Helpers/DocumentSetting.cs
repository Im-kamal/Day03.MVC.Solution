using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MVCProject.PL.Helpers
{
	public static class DocumentSettings
	{
		public static  string UploadFile(IFormFile file, string folderName)
		{
			// 1. Get Located Folder Path
			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName);
			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			// 2. Get File Name and Make it UNIQUE
			var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

			// 3. Get File Path
			var filePath = Path.Combine(folderPath, fileName);

			// 4. Save Files as Streams[Data Per Time]
			using var fileStream = new FileStream(filePath, FileMode.Create);

			 file.CopyTo(fileStream);

			return fileName;
		}

		public static void DeleteFile(string fileName, string folderName)
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName, fileName);
			if (File.Exists(filePath))
				File.Delete(filePath);
		}
	}
}















//using Microsoft.AspNetCore.Http;
//using System;
//using System.IO;

//namespace Day03.MVC.PL.Helpers
//{
//	public static class DocumentSetting
//	{
//		public static string UploadFile(IFormFile file,string FolderName)
//		{
//			//1.Get Located folder path
//			//string FolderPath = $"C:\\rout\\MVC\\Session03\\Pdf\\MVC.Solution\\Day03.MVC.PL\\wwwroot\\Files\\{FolderName}";
//			//string FolderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{{FolderName}}"; 
//			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot,Files", FolderName);  //Recommended

//			if(!Directory.Exists(folderPath)) 
//				Directory.CreateDirectory(folderPath);

//			//2.Get File Name and Make it UNIQUE

//			var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

//			//3.File Path => folder Path + File Name	
//			var filePath=Path.Combine(folderPath, fileName);

//			//4.Save Files as Streams [Data Per Time]
//			using var fileStream=new FileStream(filePath,FileMode.Create); 
//			file.CopyTo(fileStream);
//			return fileName;

//		}
//		public static void Deletefile(string fileName,string FolderName)
//		{
//			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot,Files", FolderName, fileName);

//			if(File.Exists(filePath))
//				File.Delete(filePath);
//		}

//	}
//}
