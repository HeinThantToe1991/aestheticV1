

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ACE.OmniChannel.FileSystem.Core.Remote;
using ACE.OmniChannel.Retail.UI;
using UI_Layer.Areas.BackendSystem.Models;
using UI_Layer.Globalizer;


namespace UI_Layer.Globalizer
{
    public class ImageResizeAndCompress 
    {
        
        public IConfiguration _config { get; }
        public static string _imagePath { get; set; }
        public static string _ftpPath { get; set; }
        public static IWebHostEnvironment _webHostEnvironment;

        public ImageResizeAndCompress(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            
            _config = config;
            _ftpPath = _config.GetValue<string>("ImagePath:LocalPath");
            _webHostEnvironment = webHostEnvironment;

        }

        #region " Upload with FTP "
        public static void UploadFileToFtp(string ftpUrl, string filePath)
        {
            using (var client = new WebClient())
            {
                client.UploadFile(ftpUrl, WebRequestMethods.Ftp.UploadFile, filePath);
            }
        }
        // Use This when save with FTP
        public async static Task<bool> ImageResize(IFormFile fromFile, string uploadStatus, string imageName,CompanyInformationViewModel companyInfo, IConfiguration configuration, string status = "", string savePath = "") //(savePath=Image Save Path)
        {
            try
            {
                _imagePath = (string.IsNullOrEmpty(savePath) ? configuration.GetSection("ImagePath:LocalPath").Value : savePath);
                // 001ACE will be company Code
                _imagePath = _imagePath + @"/OCRH/"+ companyInfo.CompanyName + "/Image";
                var filepath = Path.GetTempFileName();


                var path = filepath;
                var defaultPath = "";
                var resizePath = "";
                var folderPath = "";
                var defautwidth = 0; var defaultheight = 0;
                var resizewidth = 0; var resizeheight = 0;
                bool resizeStatus = false;
                decimal imageSize = Math.Round(((decimal)fromFile.Length / (decimal)1024), 2);
                var image = Image.FromStream(fromFile.OpenReadStream());
                List<string> outputDirectory = new List<String>(); // "E:\\ImageTest\\ImageOutput.jpg";
                defautwidth = 560;
                defaultheight = 560;
                resizewidth = 270;
                resizeheight = 270;

                switch (uploadStatus)
                {
                    case "Category":

                        if (defautwidth != image.Width || defaultheight != image.Height)
                        {

                            resizeStatus = true;
                        }
                        else
                        {
                            if (imageSize > 150)
                            {
                                return true;
                            }
                        }
                        resizePath = _imagePath + "/Category/" + imageName;
                        defaultPath = _imagePath + "/Category/Details/" + imageName;
                        break;
                    case "Group":

                        if (defautwidth != image.Width || defaultheight != image.Height)
                        {

                            resizeStatus = true;
                        }
                        else
                        {
                            if (imageSize > 150)
                            {
                                return true;
                            }
                        }
                        resizePath = _imagePath + "/ProductGroup/" + imageName;
                        defaultPath = _imagePath + "/ProductGroup/Details/" + imageName;
                        break;
                    case "Class":

                        if (defautwidth != image.Width || defaultheight != image.Height)
                        {

                            resizeStatus = true;
                        }
                        else
                        {
                            if (imageSize > 150)
                            {
                                return true;
                            }
                        }
                        resizePath = _imagePath + "/ProductClass/" + imageName;
                        defaultPath = _imagePath + "/ProductClass/Details/" + imageName;
                        break;
                    case "Stock":
                        if (defautwidth != image.Width || defaultheight != image.Height)
                        {

                            resizeStatus = true;
                        }
                        else
                        {
                            if (imageSize > 150)
                            {
                                return true;
                            }
                        }
                        resizePath = _imagePath + "/Product" + imageName;
                        defaultPath = _imagePath + "/Product/Details/" + imageName;
                        break;
                    case "Activity":
                        if (defautwidth != image.Width || defaultheight != image.Height)
                        {

                            resizeStatus = true;
                        }
                        else
                        {
                            if (imageSize > 150)
                            {
                                return true;
                            }
                        }
                        resizePath = _imagePath + "/Activity/" + imageName;
                        defaultPath = _imagePath + "/Activity/Details/" + imageName;
                        break;
                    case "BannerSlider":
                        defautwidth = 1114; /*1114,858*/
                        defaultheight = 418; /*418,322*/
                        resizewidth = 720;
                        resizeheight = 270;
                        if (defautwidth != image.Width || defaultheight != image.Height)
                        {
                            resizeStatus = true;
                        }
                        else
                        {
                            if (imageSize > 150)
                            {
                                return true;
                            }
                        }
                        resizePath = _imagePath + "/BannerSlider/" + imageName;
                        defaultPath = _imagePath + "/BannerSlider/Details/" + imageName;
                        break;
                    case "PaymentMethod":
                        defautwidth = 1114; /*1114,858*/
                        defaultheight = 418; /*418,322*/
                        resizewidth = 720;
                        resizeheight = 270;
                        if (defautwidth != image.Width || defaultheight != image.Height)
                        {
                            resizeStatus = true;
                        }
                        else
                        {
                            if (imageSize > 100)
                            {
                                return true;
                            }
                        }
                        resizePath = _imagePath + "\\image\\PaymentMethod\\" + imageName;
                        defaultPath = _imagePath + "\\image\\PaymentMethod\\Details\\" + imageName;
                        break;
                    case "LogIn":
                        defautwidth = 560;
                        defaultheight = 555;
                        if (defautwidth != image.Width || defaultheight != image.Height)
                        {
                            resizeStatus = true;
                        }
                        else
                        {
                            if (imageSize > 150)
                            {
                                return true;
                            }
                        }
                        //resizePath = _imagePath + "\\image\\LogIn\\" + imageName;
                        defaultPath = _imagePath + "/LogIn/Details/" + imageName;
                        break;
                    case "AdvertisementContent":
                        defautwidth = 500;
                        defaultheight = 500;
                        if (defautwidth != image.Width || defaultheight != image.Height)
                        {
                            resizeStatus = true;
                        }
                        else
                        {
                            if (imageSize > 150)
                            {
                                return true;
                            }
                        }
                        defaultPath = _imagePath + "/AdvertisementContent/Details/" + status + "/" + imageName;
                        break;
                    case "UserImage":
                        defautwidth = 560;
                        defaultheight = 555;
                        if (defautwidth != image.Width || defaultheight != image.Height)
                        {
                            resizeStatus = true;
                        }
                        else
                        {
                            if (imageSize > 150)
                            {
                                return true;
                            }
                        }
                        resizePath = _imagePath + "/UserImage/" + imageName;
                        defaultPath = _imagePath + "/UserImage/" + imageName;
                        break;
                    case "Company_Information":
                        defautwidth = 500;
                        defaultheight = 500;
                        if (defautwidth != image.Width || defaultheight != image.Height)
                        {
                            resizeStatus = true;
                        }
                        else
                        {
                            if (imageSize > 150)
                            {
                                return true;
                            }
                        }
                        defaultPath = _imagePath + "/Company_Information/"+ imageName;
                        break;


                }

                if (resizeStatus)
                {
                    var resizeds = new Bitmap(image, new Size(defautwidth, defaultheight));
                    //resizeds.Save(stream, ImageFormat.Jpeg);
                    var byteArray = ConvertBitMapToByteArray(resizeds);
                    SaveFileToFTPServer(fromFile, byteArray, defaultPath,configuration);
                }
                else
                {
                    //await fromFile.CopyToAsync(stream);
                    var byteArray = ConvertIFormFileToByteArray(fromFile);
                    SaveFileToFTPServer(fromFile, byteArray, defaultPath, configuration);
                }

                if (resizePath != "")
                {
                    var resizeImage = ResizeImage(image, resizewidth, resizeheight);
                    var byteArray = ConvertBitMapToByteArray(resizeImage);
                    SaveFileToFTPServer(fromFile, byteArray, resizePath, configuration);
                }


                return false;
            }
            catch (Exception e)
            {

                return true;
            }
        }

        private static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        public static byte[] ConvertIFormFileToByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Get a stream to the file content
                using (var stream = file.OpenReadStream())
                {
                    // Read the stream into the memory stream
                    stream.CopyTo(memoryStream);
                }
                // Return the byte array
                return memoryStream.ToArray();
            }
        }

        public static byte[] ConvertBitMapToByteArray(Bitmap bitmap)
        {
            byte[] byteArray;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Jpeg); // Change ImageFormat based on your bitmap format
                byteArray = memoryStream.ToArray();
            }
            return byteArray;
        }
        public static void SaveFileToFTPServer(IFormFile fromFile, byte[] fileContents, string filePath, IConfiguration configuration)
        {
            string folderPath = Path.GetDirectoryName(filePath);
            CreateDirectoryIfNotExists(folderPath,configuration);
            try
            {
                string host = configuration.GetSection("ImagePath:LocalPath").Value;
                string ftpUserName = configuration.GetSection("FTPUser").Value;
                string ftpPassword = configuration.GetSection("FTPPassword").Value;

                // Create a FTP request object to upload the file
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(filePath);
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpRequest.Credentials = new NetworkCredential(ftpUserName, ftpPassword);

                // Upload the file to the FTP server
                using (Stream requestStream = ftpRequest.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }

                // Display the status of the FTP upload
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                //Console.WriteLine("Upload status: {0}", response.StatusDescription);
                response.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error status: {0}", ex.GetBaseException().Message);
            }
        }
        
        public static void CreateDirectoryIfNotExists(string folderPath,IConfiguration configuration)
        {
            _ftpPath = configuration.GetSection("ImagePath:LocalPath").Value;
            _ftpPath = _ftpPath.Replace("//", "/");
            folderPath = folderPath.Replace("\\", "/");
            folderPath = folderPath.Replace(_ftpPath, "");
            //folderPath = @"/OCRH/002ATMD/Image/ProductGroup/Details";

            var setting = CommonMethods.GetFTPSetting(configuration);
            IRemoteFileSystemContext remote = new FtpRemoteFileSystem(setting);
            remote.Connect();                                       /*establish connection*/
        /*    remote.SetRootAsWorkingDirectory();       */             /*set root as work directory*/
            bool isConnected = remote.IsConnected();                /*check connection done or not*/
            remote.CreateDirectoryIfNotExists(folderPath);
            remote.Disconnect();                                    /*stop connection*/
            remote.Dispose();
        }


        
        #endregion

       



        public static void CheckAndCreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        //public async static Task<string> ImageResize_String(IFormFile fromFile, string uploadStatus, string imageName, string savePath = "") //(savePath=Image Save Path)
        //{
        //    try
        //    {

        //        _imagePath = (string.IsNullOrEmpty(savePath) ? ConfigurationHelper.config.GetSection("ImagePath:LocalPath").Value : savePath);
        //        var filepath = Path.GetTempFileName();

        //        var path = filepath;
        //        var defaultPath = "";
        //        var resizePath = "";
        //        var folderPath = "";
        //        var defautwidth = 0; var defaultheight = 0;
        //        var resizewidth = 0; var resizeheight = 0;
        //        bool resizeStatus = false;
        //        decimal imageSize = Math.Round(((decimal)fromFile.Length / (decimal)1024), 2);
        //        var image = Image.FromStream(fromFile.OpenReadStream());
        //        List<string> outputDirectory = new List<String>(); // "E:\\ImageTest\\ImageOutput.jpg";
        //        defautwidth = 560;
        //        defaultheight = 560;
        //        resizewidth = 270;
        //        resizeheight = 270;

        //        switch (uploadStatus)
        //        {
        //            case "Category":

        //                if (defautwidth != image.Width || defaultheight != image.Height)
        //                {

        //                    resizeStatus = true;
        //                }
        //                else
        //                {
        //                    if (imageSize > 100)
        //                    {
        //                        return "Image size must be less than 100KB!";
        //                    }
        //                }
        //                resizePath = _imagePath + "\\image\\Category\\" + imageName;
        //                defaultPath = _imagePath + "\\image\\Category\\Details\\" + imageName;
        //                break;
        //            case "Stock":
        //                if (defautwidth != image.Width || defaultheight != image.Height)
        //                {

        //                    resizeStatus = true;
        //                }
        //                else
        //                {
        //                    if (imageSize > 100)
        //                    {
        //                        return "Image size must be less than 100KB!";
        //                    }
        //                }
        //                resizePath = _imagePath + "\\image\\Product\\" + imageName;
        //                defaultPath = _imagePath + "\\image\\Product\\Details\\" + imageName;
        //                break;
        //            case "Activity":
        //                if (defautwidth != image.Width || defaultheight != image.Height)
        //                {

        //                    resizeStatus = true;
        //                }
        //                else
        //                {
        //                    if (imageSize > 100)
        //                    {
        //                        return "Image size must be less than 100KB!";
        //                    }
        //                }
        //                resizePath = _imagePath + "\\image\\Activity\\" + imageName;
        //                defaultPath = _imagePath + "\\image\\Activity\\Details\\" + imageName;
        //                break;
        //            case "BannerSlider":
        //                defautwidth = 1114; /*1114,858*/
        //                defaultheight = 418; /*418,322*/
        //                resizewidth = 720;
        //                resizeheight = 270;
        //                if (defautwidth != image.Width || defaultheight != image.Height)
        //                {
        //                    resizeStatus = true;
        //                }
        //                else
        //                {
        //                    if (imageSize > 100)
        //                    {
        //                        return "Image size must be less than 100KB!";
        //                    }
        //                }
        //                resizePath = _imagePath + "\\image\\BannerSlider\\" + imageName;
        //                defaultPath = _imagePath + "\\image\\BannerSlider\\Details\\" + imageName;
        //                break;
        //            case "PaymentMethod":
        //                defautwidth = 1114; /*1114,858*/
        //                defaultheight = 418; /*418,322*/
        //                resizewidth = 720;
        //                resizeheight = 270;
        //                if (defautwidth != image.Width || defaultheight != image.Height)
        //                {
        //                    resizeStatus = true;
        //                }
        //                else
        //                {
        //                    if (imageSize > 100)
        //                    {
        //                        return "Image size must be less than 100KB!";
        //                    }
        //                }
        //                resizePath = _imagePath + "\\image\\PaymentMethod\\" + imageName;
        //                defaultPath = _imagePath + "\\image\\PaymentMethod\\Details\\" + imageName;
        //                break;
        //            case "LogIn":
        //                defautwidth = 560;
        //                defaultheight = 555;
        //                if (defautwidth != image.Width || defaultheight != image.Height)
        //                {
        //                    resizeStatus = true;
        //                }
        //                else
        //                {
        //                    if (imageSize > 100)
        //                    {
        //                        return "Image size must be less than 100KB!";
        //                    }
        //                }
        //                //resizePath = _imagePath + "\\image\\LogIn\\" + imageName;
        //                defaultPath = _imagePath + "\\image\\LogIn\\Details\\" + imageName;
        //                break;
        //            case "AdvertisementContent":
        //                defautwidth = 500;
        //                defaultheight = 500;
        //                if (defautwidth != image.Width || defaultheight != image.Height)
        //                {
        //                    resizeStatus = true;
        //                }
        //                else
        //                {
        //                    if (imageSize > 150)
        //                    {
        //                        return "Image size must be less than 100KB!";
        //                    }
        //                }
        //                // Backend Image Store                        
        //                defaultPath = _imagePath + "\\image\\AdvertisementContent\\Details\\" + "\\" + imageName;
        //                break;
        //            case "UserImage":
        //                resizeStatus = false;
        //                if (imageSize > 150)
        //                {
        //                    return "Image size must be less than 100KB!";
        //                }

        //                defaultPath = _imagePath + "\\image\\UserImage\\" + imageName;
        //                //defaultPath = _imagePath + "\\image\\UserImage\\Details\\" + imageName;
        //                break;

        //        }
        //        if (System.IO.File.Exists(defaultPath))
        //        {
        //            Image image2 = Image.FromFile(defaultPath);
        //            image2.Dispose();
        //            System.GC.Collect();
        //            System.GC.WaitForPendingFinalizers();

        //            System.IO.File.Delete(defaultPath);
        //        }
        //        folderPath = Path.GetDirectoryName(defaultPath);
        //        CheckAndCreateFolder(folderPath);
        //        using (var stream = System.IO.File.Create(defaultPath)) //new FileStream(defaultPath, FileMode.Create)) // Detail
        //        {
        //            if (resizeStatus)
        //            {
        //                var resizeds = new Bitmap(image, new Size(defautwidth, defaultheight));
        //                resizeds.Save(stream, ImageFormat.Jpeg);
        //            }
        //            else
        //            {
        //                await fromFile.CopyToAsync(stream);
        //            }

        //        }
        //        if (resizePath != "")
        //        {
        //            if (System.IO.File.Exists(resizePath))
        //            {
        //                Image image2 = Image.FromFile(resizePath);
        //                image2.Dispose();
        //                System.GC.Collect();
        //                System.GC.WaitForPendingFinalizers();

        //                System.IO.File.Delete(resizePath);
        //            }
        //            using (var reSizeimage = new Bitmap(System.Drawing.Image.FromFile(defaultPath))) // Small
        //            {
        //                var resized = new Bitmap(resizewidth, resizeheight);
        //                using (var graphic = Graphics.FromImage(resized))
        //                {
        //                    graphic.CompositingQuality = CompositingQuality.HighSpeed;
        //                    graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //                    graphic.DrawImage(reSizeimage, 0, 0, resizewidth, resizeheight);

        //                    using (var output = System.IO.File.Open(resizePath, FileMode.Create))
        //                    {
        //                        var qualityParaId = Encoder.Quality;
        //                        var encoderParameters = new EncoderParameters(1);
        //                        encoderParameters.Param[0] = new EncoderParameter(qualityParaId, 60L);
        //                        var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
        //                        resized.Save(output, codec, encoderParameters);

        //                    }

        //                }
        //            }
        //        }


        //        return "";
        //    }
        //    catch (Exception e)
        //    {

        //        return e.Message;
        //    }
        //}
        // Delete Image with FPT
        public static void deleteImage(string uploadStatus, string imageName,IConfiguration configuration) 
        {
            
            string _detail = string.Empty ;
            string _default = string.Empty;
            // 001ACE will be company Code
            _imagePath =  @"/OCRH/001ACE/Image";
            switch (uploadStatus)
            {
                case "Category":
                    _detail = _imagePath + "/Category/Details/" + imageName;
                    _default = _imagePath + "/Category/" + imageName;
                    break;
                case "Group":
                    _detail = _imagePath + "/ProductGroup/Details/" + imageName;
                    _default = _imagePath + "/ProductGroup/" + imageName;
                    break;
                case "ProductClass":
                    _detail = _imagePath + "/ProductClass/Details/" + imageName;
                    _default = _imagePath + "/ProductClass/" + imageName;
                    break;
                case "Stock":
                    _default = _imagePath + "/Product/" + imageName;
                    _detail = _imagePath + "/Product/Details/" + imageName;
                    break;
                case "Activity":
                    _default = _imagePath + "/Activity/" + imageName;
                    _detail = _imagePath + "/Activity/Details/" + imageName;
                    break;
                case "BannerSlider":
                    _default = _imagePath + "/BannerSlider/" + imageName;
                    _detail = _imagePath + "/BannerSlider/Details/" + imageName;
                    break;
                case "LogIn":
                    //_detail = _imagePath + "/image/LogIn/" + imageName;
                    _detail = _imagePath + "/LogIn/Details/" + imageName;
                    break;

            }

            try
            {
                var setting = CommonMethods.GetFTPSetting(configuration);
                IRemoteFileSystemContext remote = new FtpRemoteFileSystem(setting);
                remote.Connect();                                       /*establish connection*/
                bool isConnected = remote.IsConnected();                /*check connection done or not*/
                remote.DeleteFileIfExists(_default);
                remote.DeleteFileIfExists(_detail);
                remote.Disconnect();                                    /*stop connection*/
                remote.Dispose();
            }
            catch(Exception ex)
            {

            }
           
        }

        
        public static string ImageToBase64(string _imagePath)
        {
            string _base64String = null;

            using (System.Drawing.Image _image = System.Drawing.Image.FromFile(_imagePath))
            {
                using (MemoryStream _mStream = new MemoryStream())
                {
                    _image.Save(_mStream, _image.RawFormat);
                    byte[] _imageBytes = _mStream.ToArray();
                    _base64String = Convert.ToBase64String(_imageBytes);

                    return "data:image/jpg;base64," + _base64String;
                }
            }
        }

        public static System.Drawing.Image ResizeImage(IFormFile fromFile, Size size)
        {
            var imgToResize = Image.FromStream(fromFile.OpenReadStream());
            //Get the image current width  
            int sourceWidth = imgToResize.Width;
            //Get the image current height  
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size  
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size  
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width  
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height  
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }


        // Use This when save with HTTP
        //public async static Task<bool> ImageResize(IFormFile fromFile, string uploadStatus, string imageName, string status = "", string savePath = "") //(savePath=Image Save Path)
        //{
        //    try
        //    {

        //        _imagePath = (string.IsNullOrEmpty(savePath) ? ConfigurationHelper.config.GetSection("ImagePath:LocalPath").Value : savePath);
        //        var filepath = Path.GetTempFileName();

        //        var path = filepath;
        //        var defaultPath = "";
        //        var resizePath = "";
        //        var folderPath = "";
        //        var defautwidth = 0; var defaultheight = 0;
        //        var resizewidth = 0; var resizeheight = 0;
        //        bool resizeStatus = false;
        //        decimal imageSize = Math.Round(((decimal)fromFile.Length / (decimal)1024), 2);
        //        var image = Image.FromStream(fromFile.OpenReadStream());
        //        List<string> outputDirectory = new List<String>(); // "E:\\ImageTest\\ImageOutput.jpg";
        //        defautwidth = 560;
        //        defaultheight = 560;
        //        resizewidth = 270;
        //        resizeheight = 270;

        //        switch (uploadStatus)
        //        {
        //            case "Category":

        //                if (defautwidth != image.Width || defaultheight != image.Height)
        //                {

        //                    resizeStatus = true;
        //                }
        //                else
        //                {
        //                    if (imageSize > 150)
        //                    {
        //                        return true;
        //                    }
        //                }
        //                resizePath = _imagePath + "\\image\\Category\\" + imageName;
        //                defaultPath = _imagePath + "\\image\\Category\\Details\\" + imageName;
        //                break;
        //            case "Group":

        //                if (defautwidth != image.Width || defaultheight != image.Height)
        //                {

        //                    resizeStatus = true;
        //                }
        //                else
        //                {
        //                    if (imageSize > 150)
        //                    {
        //                        return true;
        //                    }
        //                }
        //                resizePath = _imagePath + "\\image\\ProductGroup\\" + imageName;
        //                defaultPath = _imagePath + "\\image\\ProductGroup\\Details\\" + imageName;
        //                break;
        //            case "Class":

        //                if (defautwidth != image.Width || defaultheight != image.Height)
        //                {

        //                    resizeStatus = true;
        //                }
        //                else
        //                {
        //                    if (imageSize > 150)
        //                    {
        //                        return true;
        //                    }
        //                }
        //                resizePath = _imagePath + "\\image\\ProductClass\\" + imageName;
        //                defaultPath = _imagePath + "\\image\\ProductClass\\Details\\" + imageName;
        //                break;
        //            case "Stock":
        //                if (defautwidth != image.Width || defaultheight != image.Height)
        //                {

        //                    resizeStatus = true;
        //                }
        //                else
        //                {
        //                    if (imageSize > 150)
        //                    {
        //                        return true;
        //                    }
        //                }
        //                resizePath = _imagePath + "\\image\\Product\\" + imageName;
        //                defaultPath = _imagePath + "\\image\\Product\\Details\\" + imageName;
        //                break;
        //            case "Activity":
        //                if (defautwidth != image.Width || defaultheight != image.Height)
        //                {

        //                    resizeStatus = true;
        //                }
        //                else
        //                {
        //                    if (imageSize > 150)
        //                    {
        //                        return true;
        //                    }
        //                }
        //                resizePath = _imagePath + "\\image\\Activity\\" + imageName;
        //                defaultPath = _imagePath + "\\image\\Activity\\Details\\" + imageName;
        //                break;
        //            case "BannerSlider":
        //                defautwidth = 1114; /*1114,858*/
        //                defaultheight = 418; /*418,322*/
        //                resizewidth = 720;
        //                resizeheight = 270;
        //                if (defautwidth != image.Width || defaultheight != image.Height)
        //                {
        //                    resizeStatus = true;
        //                }
        //                else
        //                {
        //                    if (imageSize > 150)
        //                    {
        //                        return true;
        //                    }
        //                }
        //                resizePath = _imagePath + "\\image\\BannerSlider\\" + imageName;
        //                defaultPath = _imagePath + "\\image\\BannerSlider\\Details\\" + imageName;
        //                break;
        //            case "LogIn":
        //                defautwidth = 560;
        //                defaultheight = 555;
        //                if (defautwidth != image.Width || defaultheight != image.Height)
        //                {
        //                    resizeStatus = true;
        //                }
        //                else
        //                {
        //                    if (imageSize > 150)
        //                    {
        //                        return true;
        //                    }
        //                }
        //                //resizePath = _imagePath + "\\image\\LogIn\\" + imageName;
        //                defaultPath = _imagePath + "\\image\\LogIn\\Details\\" + imageName;
        //                break;
        //            case "AdvertisementContent":
        //                defautwidth = 500;
        //                defaultheight = 500;
        //                if (defautwidth != image.Width || defaultheight != image.Height)
        //                {
        //                    resizeStatus = true;
        //                }
        //                else
        //                {
        //                    if (imageSize > 150)
        //                    {
        //                        return true;
        //                    }
        //                }
        //                // Backend Image Store
        //                //_imagePath = _webHostEnvironment.WebRootPath;
        //                _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        //                //resizePath =_imagePath + "\\image\\AdvertisementContent\\" + imageName;
        //                defaultPath = _imagePath + "\\image\\AdvertisementContent\\Details\\" + status + "\\" + imageName;
        //                break;
        //            case "UserImage":
        //                defautwidth = 560;
        //                defaultheight = 555;
        //                if (defautwidth != image.Width || defaultheight != image.Height)
        //                {
        //                    resizeStatus = true;
        //                }
        //                else
        //                {
        //                    if (imageSize > 150)
        //                    {
        //                        return true;
        //                    }
        //                }
        //                resizePath = _imagePath + "\\image\\UserImage\\" + imageName;
        //                defaultPath = _imagePath + "\\image\\UserImage\\Details\\" + imageName;
        //                break;



        //        }

        //        if (System.IO.File.Exists(defaultPath))
        //        {
        //            Image image2 = Image.FromFile(defaultPath);
        //            image2.Dispose();
        //            System.GC.Collect();
        //            System.GC.WaitForPendingFinalizers();

        //            System.IO.File.Delete(defaultPath);
        //        }
        //        folderPath = Path.GetDirectoryName(defaultPath);
        //        CheckAndCreateFolder(folderPath);
        //        using (var stream = System.IO.File.Create(defaultPath)) //new FileStream(defaultPath, FileMode.Create)) // Detail
        //        {
        //            if (resizeStatus)
        //            {
        //                var resizeds = new Bitmap(image, new Size(defautwidth, defaultheight));
        //                resizeds.Save(stream, ImageFormat.Jpeg);
        //            }
        //            else
        //            {
        //                await fromFile.CopyToAsync(stream);
        //            }

        //        }



        //        if (resizePath != "")
        //        {
        //            if (System.IO.File.Exists(resizePath))
        //            {
        //                Image image2 = Image.FromFile(resizePath);
        //                image2.Dispose();
        //                System.GC.Collect();
        //                System.GC.WaitForPendingFinalizers();

        //                System.IO.File.Delete(resizePath);
        //            }
        //            folderPath = Path.GetDirectoryName(resizePath);
        //            CheckAndCreateFolder(folderPath);
        //            using (var reSizeimage = new Bitmap(System.Drawing.Image.FromFile(defaultPath))) // Small
        //            {
        //                var resized = new Bitmap(resizewidth, resizeheight);
        //                using (var graphic = Graphics.FromImage(resized))
        //                {
        //                    graphic.CompositingQuality = CompositingQuality.HighSpeed;
        //                    graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //                    graphic.DrawImage(reSizeimage, 0, 0, resizewidth, resizeheight);

        //                    using (var output = System.IO.File.Open(resizePath, FileMode.Create))
        //                    {
        //                        var qualityParaId = Encoder.Quality;
        //                        var encoderParameters = new EncoderParameters(1);
        //                        encoderParameters.Param[0] = new EncoderParameter(qualityParaId, 60L);
        //                        var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
        //                        resized.Save(output, codec, encoderParameters);

        //                    }


        //                }
        //            }
        //        }


        //        return false;
        //    }
        //    catch (Exception e)
        //    {

        //        return true;
        //    }
        //}

        // Use This when Delete with HTTP
        //public static void deleteImage(string uploadStatus, string imageName)
        //{
        //    _imagePath = ConfigurationHelper.config.GetSection("ImagePath:LocalPath").Value;
        //    string _detail = string.Empty;
        //    string _default = string.Empty;
        //    switch (uploadStatus)
        //    {
        //        case "Category":
        //            _detail = _imagePath + "\\image\\Category\\Details\\" + imageName;
        //            _default = _imagePath + "\\image\\Category\\" + imageName;
        //            break;
        //        case "Stock":
        //            _default = _imagePath + "\\image\\Product\\" + imageName;
        //            _detail = _imagePath + "\\image\\Product\\Details\\" + imageName;
        //            break;
        //        case "Activity":
        //            _default = _imagePath + "\\image\\Activity\\" + imageName;
        //            _detail = _imagePath + "\\image\\Activity\\Details\\" + imageName;
        //            break;
        //        case "BannerSlider":
        //            _default = _imagePath + "\\image\\BannerSlider\\" + imageName;
        //            _detail = _imagePath + "\\image\\BannerSlider\\Details\\" + imageName;
        //            break;
        //        case "LogIn":
        //            //_detail = _imagePath + "\\image\\LogIn\\" + imageName;
        //            _detail = _imagePath + "\\image\\LogIn\\Details\\" + imageName;
        //            break;

        //    }
        //    if (System.IO.File.Exists(_detail))
        //    {
        //        Image image2 = Image.FromFile(_detail);
        //        image2.Dispose();
        //        System.GC.Collect();
        //        System.GC.WaitForPendingFinalizers();
        //        System.IO.File.Delete(_detail);
        //    }
        //    if (System.IO.File.Exists(_default))
        //    {
        //        Image image2 = Image.FromFile(_default);
        //        image2.Dispose();
        //        System.GC.Collect();
        //        System.GC.WaitForPendingFinalizers();
        //        System.IO.File.Delete(_default);
        //    }
        //}
    }
}
