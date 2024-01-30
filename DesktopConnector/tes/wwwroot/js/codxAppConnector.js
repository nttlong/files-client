function doEditInDesktopAppAsync(data) {
    
    return new Promise((resolve, reject) => {
        try {
            
            var ws = undefined
            try {
                var ws = new WebSocket("ws://127.0.0.1:8765"); // Liên lạc với ứng dụng
            }
            catch (ex) {
                throw {
                    error_code: "AppNotInstall",
                    error_message:"Codx desktop application was not run or not install"
                }
            }
            ws.onopen = () => {
                ws.send(JSON.stringify(data)); //Chuyển yêu cầu đến ứng dụng
                
            };
            ws.onmessage = function (event) {
                var data = event.data; // Nhận thông tin từ ứng dụng
                try {
                    var result = JSON.parse(event.data);
                    if (result.error_code) { //nếu là lỗi bên trong ứng dụng
                        reject(result); // Raise lỗi
                    }
                    else {
                        resolve(result); // Đã giải quyết
                    }
                }
                catch (error) {
                    reject(error); // các lỗi không xác định khác
                }
            };
        } catch (error) {
            // If failed, call reject with the error
            reject(error);
        }
    });
}
//example:
async function fromLvFile() {
    var accessToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhcHBsaWNhdGlvbiI6ImFkbWluIiwidXNlcm5hbWUiOiJyb290In0.YxVxDRWAgue0CF6PESOEZYYWgHrc-lDWgyg_UEZWqpI'
    ret = await doEditInDesktopAppAsync(
        {
            resourceExt: 'docx', //Loại tài liệu ví dụ trong trường hợp này là MS Word
            src: {
                url: 'http://172.16.7.99/lvfile/api/files/check_out_source', // Nguồn lấy nội dung file, ở ví dụ này lấy từ Lv file service
                method: 'post', // Request methiod từ nguồn là post
                header: {
                    "Authorization": "Bearer " + accessToken // Do nguồn lấy nội dung đòi hỏi phải có chứng thực bằng Bearer AccessToken
                },
                data: {
                    appName: "lv-docs", //Do API http://172.16.7.99/lvfile/api/files/check_out_source đòi hỏi phải có data với aappName
                    uploadId: 'f0f94530-b7f8-43c8-9bed-515725e24b66' // và uploadId
                }
            },
            dst: {
                url: 'http://172.16.7.99/lvfile/api/files/check_in_source', // Nguồn cập nhật nội dung, ở ví dụ này cập nhật vào server 99
                method: 'post',
                header: {
                    "Authorization": "Bearer " + accessToken // Do nguồn cập nhật nội dung đòi hỏi phải có chứng thực bằng Bearer AccessToken
                },
                data: {
                    appName: "lv-docs",
                    uploadId: 'f0f94530-b7f8-43c8-9bed-515725e24b66'
                },
                fileSource: "content"
            }
        }
    )
}
async function fromCodxToLvFileService() {
    var CodxAccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im50dGxvbmciLCJuYW1laWQiOiJudHRsb25nIiwiZW1haWwiOiJudHRsb25nQGxhY3ZpZXQuY29tLnZuIiwiRnVsbE5hbWUiOiJOZ3V54buFbiBUcuG6p24gVGjhur8gTG9uZyIsIk1vYmlsZSI6IiIsImp0aSI6IjEyYzRiZGVlLTY3ZGItNGNmMS1iODhhLWZhYTAzODNmZmJkYSIsInNrIjoiYzk1MzQwMGMtOTk2ZS00MjNiLTgxMzktYzc3YzlkOTMxM2E4IiwicnNrIjoiYzk1MzQwMGMtOTk2ZS00MjNiLTgxMzktYzc3YzlkOTMxM2E4IiwibHQiOiIxIiwiaXAiOiIxNzIuMTYuOS42NCIsIm5iZiI6MTcwMzgxNDMxMywiZXhwIjoxNzM1MzUwMzEzLCJpYXQiOjE3MDM4MTQzMTMsImlzcyI6ImVybS5sYWN2aWV0LnZuIiwiYXVkIjoiZXJtLmxhY3ZpZXQudm4ifQ.jZ1Ecr8cXm4Z_DCwrY-xJPBKZpvcR0zwmDiCVoFHZfU"
    var lvFileServiceAccessToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhcHBsaWNhdGlvbiI6ImFkbWluIiwidXNlcm5hbWUiOiJyb290In0.YxVxDRWAgue0CF6PESOEZYYWgHrc-lDWgyg_UEZWqpI'

    ret = await doEditInDesktopAppAsync(
        {
            resourceExt: 'docx', //Loại tài liệu ví dụ trong trường hợp này là MS Word
            src: {
                url: 'https://codx.lacviet.vn/api/DM/exec36', // Nguồn lấy nội dung file, ở ví dụ này lấy từ Codx
                method: 'post', // Request methiod từ nguồn là post
                header: {
                    "Lvtk": CodxAccessToken // Do nguồn lấy nội dung đòi hỏi phải có chứng thực bằng Bearer AccessToken
                },
                data: { //Nguồn lấy từ Codx 
                    assemblyName: "DM",
                    className: "FileBussiness",
                    functionID: "WP",
                    isJson: true,
                    localRegion: "en-US",
                    localTz: "Asia/Bangkok",
                    methodName: "GetFilesByGridModelAsync",
                    msgBodyData: [{ pageLoading: true, page: 1, pageSize: 5, entityName: "DM_FileInfo" }],
                    saas: 0,
                    service: "DM",
                    tenant: "default",
                    userID: "nttlong"
                }
            },
            dst: {
                url: 'http://172.16.7.99/lvfile/api/files/check_in_source', // Nguồn cập nhật nội dung, ở ví dụ này cập nhật vào server 99
                method: 'post',
                header: {
                    "Authorization": "Bearer " + lvFileServiceAccessToken // Do nguồn cập nhật nội dung đòi hỏi phải có chứng thực bằng Bearer AccessToken
                },
                data: {
                    appName: "lv-docs",
                    uploadId: 'f0f94530-b7f8-43c8-9bed-515725e24b66'
                }
            }
        }
    )
}