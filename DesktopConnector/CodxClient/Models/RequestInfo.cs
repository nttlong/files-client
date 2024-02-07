using System.Diagnostics;
using System.Security.Cryptography;

namespace CodxClient.Models
{
    public enum RequestInfoStatusEnum
    {
        IsUploading,
        Unknown,
        Opening
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

        public RequestInfo() {
            this.Status = RequestInfoStatusEnum.Unknown;
        }
        public DelelegateInfo? Src { get; set; }
        public DelelegateInfo? Dst { get; set; }
        public string? ResourceExt { get; set; }
        public string? HashContent { get; set; }
        public string? FilePath { get; set; }
        public string? TrackFilePath { get; set; }
        public string? RequestId { get; set; }
        public RequestInfoStatusEnum Status { get;  set; }
        public List<string> HashContentList { get;  set; }
        public string RequestData { get;  set; }
        public long SizeOfFile { get;  set; }

        public async Task<List<string>> GetHashContentOnlineAsync(int StepSize=5)
        {
            var ret = await Task.Run(() =>
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

                try
                {
                    using (var fileStream = File.OpenRead(FilePath))
                    {
                        while (bytesRead < totalLength)
                        {
                            long bytesToRead = Math.Min(hashSize, totalLength - bytesRead);
                            var buffer = new byte[bytesToRead];
                            int bytesReadThisTime = fileStream.Read(buffer, 0, (int)bytesToRead);

                            if (bytesReadThisTime == 0)
                            {
                                break;
                            }

                            bytesRead += bytesReadThisTime;
                            long skipTo = (bytesRead / StepSize) * StepSize;
                            fileStream.Seek(skipTo, SeekOrigin.Begin);

                            var hash = hashAlgorithm.ComputeHash(buffer);
                            retList.Add(Convert.ToHexString(hash));
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle file access exceptions appropriately (e.g., log the error)
                    Console.WriteLine($"Error occurred while hashing file: {ex.Message}");
                }

                return retList;
            });
            return ret;
        }
        public long GetSizeOnline()
        {
            return new FileInfo(this.FilePath).Length;
        }

        public async Task<bool> CheckIsChangeAsync()
        {
            if (this.SizeOfFile == 0)
            {
                return false;
            }
            if(this.Status!=RequestInfoStatusEnum.Unknown)
            {
                return false;
            }
            if (this.SizeOfFile != this.GetSizeOnline())
            {
                return true;
            }
            var onlineHashes = await this.GetHashContentOnlineAsync();
            if (onlineHashes.Count != this.HashContentList.Count)
                return true;
            for(var i=0;i<onlineHashes.Count;i++)
            {
                var onlineHash = onlineHashes[i];
                if (onlineHash != this.HashContentList[i])
                {
                    return true;
                }
            }
            return false;
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
                if(File.Exists(this.TrackFilePath))
                {
                    File.Delete(this.TrackFilePath);
                }
                
            }
            catch (Exception ex)
            {

            }
            try
            {
                if(File.Exists(this.FilePath)) {
                    File.Delete(this.FilePath);
                }
                
            }
            catch (Exception ex)
            {

            }
        }
    }
}
    
