using System.Diagnostics;
using System.Security.Cryptography;

namespace CodxClient.Models
{
    public enum ChangeTypeEnum
    {
        None,
        Size,
        CheckContentFail,
        Content
    }
    
    public enum RequestInfoStatusEnum
    {
        IsUploading,
        Unknown,
        Opening,
        Loading,
        Ready
    }
    /// <summary>
    /// {
    /// "resourceExt":"pdf",
    /// "src":{"url":"http://172.16.7.99/lvfile/api/files/check_out_source",
    /// "method":"post",
    /// "header":{"Authorization":"Bearer eyJhbGciOiJIUzI1"},
    /// "data":{"appName":"developer","uploadId":"17d01338-f01f-4f64-abff-db32d4b3c4e4"}},
    /// "dst":{"url":"http://172.16.7.99/lvfile/api/files/check_in_source",
    /// "method":"post","header":{"Authorization":"Bearer eyJh"},
    /// "data":{"appName":"developer","uploadId":"17d01338-f01f-4f64-abff-db32d4b3c4e4"}}}	
    /// </summary>
    public class RequestInfo
    {
        internal Process Applicaion;

        public RequestInfo()
        {
            this.Status = RequestInfoStatusEnum.Unknown;
        }
        public DelelegateInfo? Src { get; set; }
        public DelelegateInfo? Dst { get; set; }
        public string? ResourceExt { get; set; }
        public string? HashContent { get; set; }
        public string? FilePath { get; set; }
        public string OriginalFilePath { get; private set; }
        public string? TrackFilePath { get; set; }
        public string? RequestId { get; set; }
        public RequestInfoStatusEnum Status { get; set; }
        public List<string> HashContentList { get; set; }
        public string RequestData { get; set; }
        public long SizeOfFile { get; set; }
        public DateTime? LastModified { get; internal set; }

        public async Task<List<string>> GetHashContentOnlineAsync(int StepSize = 5)
        {
            try
            {
                return await this.tryGetHashContents(StepSize);
            }
            catch {
                return new List<string>();
            }

        }

        private async Task<List<string>> tryGetHashContents(int StepSize = 5)
        {
            if (new FileInfo(this.FilePath).Length == 0)
            {
                return new List<string>();
            }

            var hashSize = 1024; // Consider adjusting based on hashing algorithm and performance needs
            var hashAlgorithm = SHA256.Create(); // Replace with suitable hashing algorithm if necessary

            List<string> retList = new List<string>();
            long bytesRead = 0;
            long totalLength = new FileInfo(FilePath).Length;
            var skipSize = (int)(totalLength / StepSize);
            long skipTo = 0;
            long readStep = 0;
            var mode = new FileStreamOptions();
            mode.Options = FileOptions.Asynchronous;
            mode.Access = FileAccess.Read;
            mode.BufferSize = 1024 * 100;
            mode.Share = FileShare.Read;

            using (var fileStream = new FileStream(this.FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                //BinaryReader reader = new BinaryReader(fileStream);
                //byte[] data = reader.ReadBytes(1024);
                retList = new List<string>();
                skipTo = readStep * skipSize;
                fileStream.Seek(skipTo, SeekOrigin.Begin);
                long bytesToRead = Math.Min(hashSize, totalLength - bytesRead);
                var buffer = new byte[bytesToRead];
                int bytesReadThisTime = await fileStream.ReadAsync(buffer, 0, (int)bytesToRead);

                while ((bytesReadThisTime > 0) && (readStep < StepSize))
                {
                    var hash = hashAlgorithm.ComputeHash(buffer);
                    retList.Add(Convert.ToHexString(hash));
                    readStep++;
                    skipTo = readStep * skipSize;
                    fileStream.Seek(skipTo, SeekOrigin.Begin);
                    bytesReadThisTime = await fileStream.ReadAsync(buffer, 0, (int)bytesToRead);
                }
                fileStream.Close();
                //using (var fileStream = File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                //{


                //}
                return retList;
            }
        }

        public long GetSizeOnline()
        {
            return new FileInfo(this.FilePath).Length;
        }

        public async Task<ChangeTypeEnum> CheckIsChangeAsync()
        {
            if (!this.CheckIsReadyAccessing())
            {
                return ChangeTypeEnum.None;
            }
            if (this.SizeOfFile == 0)
            {
                return ChangeTypeEnum.None;
            }
            if (this.Status != RequestInfoStatusEnum.Ready)
            {
                return ChangeTypeEnum.None;
            }
            if (this.SizeOfFile != this.GetSizeOnline())
            {
                return ChangeTypeEnum.Size;
            }
            var onlineHashes = new List<string>();
            try
            {
                onlineHashes = await this.GetHashContentOnlineAsync();
            }
            catch (IOException)
            {
                return ChangeTypeEnum.CheckContentFail; 
            }
            if (onlineHashes.Count == 0)
            {
                return ChangeTypeEnum.None;
            }
            if (onlineHashes.Count != this.HashContentList.Count)
                return ChangeTypeEnum.Content;
            for (var i = 0; i < onlineHashes.Count; i++)
            {
                var onlineHash = onlineHashes[i];
                if (onlineHash != this.HashContentList[i])
                {
                    return ChangeTypeEnum.Content;
                }
            }
            return ChangeTypeEnum.None;
        }

        private bool CheckIsReadyAccessing()
        {
            //try
            //{
            //    using (FileStream stream = File.Open(this.FilePath, FileMode.Open, FileAccess.Read, FileShare.None))
            //    {
            //        return true;
            //    }
            //}
            //catch (IOException)
            //{
            //    return false;
            //}
            return true;
        }

        public async Task CommitAsync()
        {
            this.SizeOfFile = this.GetSizeOnline();
            this.HashContentList = await this.GetHashContentOnlineAsync();
            this.Status = RequestInfoStatusEnum.Unknown;
        }

        public async Task SaveAsync()
        {
            var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            await File.WriteAllTextAsync(this.TrackFilePath, jsonData);
        }

        internal void Delete()
        {
            try
            {
                if (File.Exists(this.TrackFilePath))
                {
                    File.Delete(this.TrackFilePath);
                }

            }
            catch (Exception ex)
            {

            }
            try
            {
                if (File.Exists(this.FilePath))
                {
                    File.Delete(this.FilePath);
                }

            }
            catch (Exception ex)
            {

            }
        }

        internal void Reset()
        {
            this.SizeOfFile = 0;
            //this.HashContentList = null;

        }

        internal void CloneToTempFile(string TmpDir)
        {
            var tempFilePath = Path.Combine(TmpDir, Guid.NewGuid().ToString());
            using (FileStream originalStream = new FileStream(this.FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (FileStream tempStream = File.OpenWrite(tempFilePath))
            {
                // Read from original file and write to temporary file
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = originalStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    tempStream.Write(buffer, 0, bytesRead);
                }
            }
            
            this.OriginalFilePath = FilePath;
            this.FilePath = tempFilePath;
        }
    }
}

