using ASPNET_API.Application.DTOs;
using ASPNET_API.Domain.Entities.IELTS;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_API.Application.Services.Interfa
{
    public interface IQuestionBankUtilService
    {
        public Task UploadFileReading(UpdatePicture updatePictureModel, string fileName, string extension);
        public Task UploadFileWriting(UpdatePicture updatePictureModel, string fileName, string extension);
        public Task UploadFileListening(UpdatePicture updatePictureModel, string fileName, string extension);
        public Task UploadFileAudio(IFormFile? fileUploads, int questionBankId);

        public Task<List<Part>> RetrieveExcelDataAsync(IFormFile? fileUploads);
        public Task<double> CalculateBandScore(int correctAnswers, string testType);
        public Task<double> CalculateOverall(double reading, double listening, double writing, double speaking);
    }
}
