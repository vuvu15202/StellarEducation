using ASPNET_API.Application.DTOs;
using ASPNET_API.Application.Services.Interfa;
using ASPNET_API.Domain.Entities.IELTS;
using ASPNET_API.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ASPNET_API.Application.Services
{
    public class QuestionBankUtilService : IQuestionBankUtilService
    {
        private readonly DonationWebApp_v2Context _context;
        private readonly IConfiguration _configuration;

        public QuestionBankUtilService(DonationWebApp_v2Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task UploadFileReading(UpdatePicture updatePictureModel, string fileName, string extension)
        {
            try
            {
                if (!string.IsNullOrEmpty(updatePictureModel.PartNo) && string.IsNullOrEmpty(updatePictureModel.GroupNo) && updatePictureModel.FileUploads != null)
                {
                    var questionBank = await _context.QuestionBanks.FindAsync(updatePictureModel.QuestionBankId);
                    if (questionBank != null)
                    {
                        Reading reading = questionBank.ReadingJSON;
                        reading.Parts[Int32.Parse(updatePictureModel.PartNo) - 1].FileURL = _configuration["URL:BackendURL"] + "/uploads/ielts/" + fileName;
                        reading.Parts[Int32.Parse(updatePictureModel.PartNo) - 1].FileType = extension.ToLower().Replace(".", "");

                        questionBank.Reading = JsonSerializer.Serialize(reading);
                        _context.QuestionBanks.Update(questionBank);
                        await _context.SaveChangesAsync();
                    }
                }

                if (!string.IsNullOrEmpty(updatePictureModel.PartNo) && !string.IsNullOrEmpty(updatePictureModel.GroupNo) && updatePictureModel.FileUploads != null)
                {
                    var questionBank = await _context.QuestionBanks.FindAsync(updatePictureModel.QuestionBankId);
                    if (questionBank != null)
                    {
                        Reading reading = questionBank.ReadingJSON;
                        reading.Parts[Int32.Parse(updatePictureModel.PartNo) - 1]
                            .Groups[Int32.Parse(updatePictureModel.GroupNo) - 1]
                            .FileURL = _configuration["URL:BackendURL"] + "/uploads/ielts/" + fileName;
                        reading.Parts[Int32.Parse(updatePictureModel.PartNo) - 1]
                            .Groups[Int32.Parse(updatePictureModel.GroupNo) - 1]
                            .FileType = extension.ToLower().Replace(".", "");

                        questionBank.Reading = JsonSerializer.Serialize(reading);
                        _context.QuestionBanks.Update(questionBank);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected Error, please try again!");
            }
        }


        public async Task UploadFileListening(UpdatePicture updatePictureModel, string fileName, string extension)
        {
            try
            {
                if (!string.IsNullOrEmpty(updatePictureModel.PartNo) && !string.IsNullOrEmpty(updatePictureModel.GroupNo) && updatePictureModel.FileUploads != null)
                {
                    var questionBank = await _context.QuestionBanks.FindAsync(updatePictureModel.QuestionBankId);
                    if (questionBank != null)
                    {
                        Listening listening = questionBank.ListeningJSON;
                        listening.Parts[Int32.Parse(updatePictureModel.PartNo) - 1]
                            .Groups[Int32.Parse(updatePictureModel.GroupNo) - 1]
                            .FileURL = _configuration["URL:BackendURL"] + "/uploads/ielts/" + fileName;
                        listening.Parts[Int32.Parse(updatePictureModel.PartNo) - 1]
                            .Groups[Int32.Parse(updatePictureModel.GroupNo) - 1]
                            .FileType = extension.ToLower().Replace(".", "");

                        questionBank.Listening = JsonSerializer.Serialize(listening);
                        _context.QuestionBanks.Update(questionBank);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected Error, please try again!");
            }
        }

        public async Task UploadFileWriting(UpdatePicture updatePictureModel, string fileName, string extension)
        {
            try
            {
                if (!string.IsNullOrEmpty(updatePictureModel.PartNo) && string.IsNullOrEmpty(updatePictureModel.GroupNo) && updatePictureModel.FileUploads != null)
                {
                    var questionBank = await _context.QuestionBanks.FindAsync(updatePictureModel.QuestionBankId);
                    if (questionBank != null)
                    {
                        Writing writing = questionBank.WritingJSON;
                        writing.Parts[Int32.Parse(updatePictureModel.PartNo) - 1].FileURL = _configuration["URL:BackendURL"] + "/uploads/ielts/" + fileName;
                        writing.Parts[Int32.Parse(updatePictureModel.PartNo) - 1].FileType = extension.ToLower().Replace(".", "");

                        questionBank.Writing = JsonSerializer.Serialize(writing);
                        _context.QuestionBanks.Update(questionBank);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected Error, please try again!");
            }
        }

        public async Task<List<Part>> RetrieveExcelDataAsync(IFormFile? FileUploads)
        {
            List<Part> parts = new List<Part>();

            if (FileUploads != null && FileUploads.Length != 0)
            {
                using (var stream = new MemoryStream())
                {
                    await FileUploads.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        var workbook = package.Workbook;

                        var worksheetParts = workbook.Worksheets["Parts"];
                        if (worksheetParts != null)
                        {
                            var rowcount = worksheetParts.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                try
                                {
                                    var partNo = worksheetParts.Cells[row, 1].Value?.ToString()?.Trim();
                                    if (!Regex.IsMatch(partNo, @"^\d+$")) throw new Exception($"Vui lòng xem lại trang tính {worksheetParts.Name}, dòng {row}, partNo là dữ liệu kiểu số!");
                                    parts.Add(
                                        new Part()
                                        {
                                            PartNo = worksheetParts.Cells[row, 1].Value?.ToString()?.Trim(),
                                            FileURL = worksheetParts.Cells[row, 2].Value?.ToString()?.Trim(),
                                            FileType = worksheetParts.Cells[row, 3].Value?.ToString()?.Trim(),
                                            QuestionRange = worksheetParts.Cells[row, 4].Value?.ToString()?.Trim(),
                                            Groups = new List<ASPNET_API.Domain.Entities.IELTS.Group>()
                                        }
                                    );
                                }
                                catch (Exception ex)
                                {
                                    //throw new Exception($"Xem lại trang tính {worksheetParts.Name}, dòng {row}");
                                    throw new Exception($"Vui lòng xem lại trang tính {worksheetParts.Name}, dòng {row}, nhập sai dữ liệu hoặc PartNo hoặc GroupNo không tồn tại!");
                                }
                            }
                        }
                        var worksheetGroups = workbook.Worksheets["Groups"];
                        if (worksheetGroups != null)
                        {
                            var rowcount = worksheetGroups.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                try
                                {
                                    var groupNo = worksheetGroups.Cells[row, 1].Value?.ToString()?.Trim();
                                    if (!Regex.IsMatch(groupNo, @"^\d+$")) throw new Exception($"Vui lòng xem lại trang tính {worksheetGroups.Name}, dòng {row}, GroupNo là dữ liệu kiểu số!");
                                    parts[int.Parse(worksheetGroups.Cells[row, 1].Value?.ToString()?.Trim()) - 1].Groups.Add(
                                    new ASPNET_API.Domain.Entities.IELTS.Group()
                                    {
                                        GroupNo = worksheetGroups.Cells[row, 2].Value?.ToString()?.Trim(),
                                        Title = worksheetGroups.Cells[row, 3].Value?.ToString()?.Trim(),
                                        Type = worksheetGroups.Cells[row, 4].Value?.ToString()?.Trim(),
                                        FileURL = worksheetGroups.Cells[row, 5].Value?.ToString()?.Trim(),
                                        FileType = worksheetGroups.Cells[row, 6].Value?.ToString()?.Trim(),
                                        QuestionRange = worksheetGroups.Cells[row, 7].Value?.ToString()?.Trim(),
                                        Questions = new List<Question>()
                                    }
                                );
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception($"Vui lòng xem lại trang tính {worksheetGroups.Name}, dòng {row}, nhập sai dữ liệu hoặc PartNo hoặc GroupNo không tồn tại!");
                                }
                            }
                        }

                        var worksheetQuestions = workbook.Worksheets["Questions"];
                        if (worksheetQuestions != null)
                        {
                            var rowcount = worksheetQuestions.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                try
                                {
                                    var questionNo = worksheetQuestions.Cells[row, 3].Value?.ToString()?.Trim();

                                    if (string.IsNullOrEmpty(questionNo) || !Regex.IsMatch(questionNo, @"^\d+$"))
                                    {
                                        throw new Exception($"Vui lòng xem lại trang tính {worksheetQuestions.Name}, dòng {row}, questionNo phải là dữ liệu kiểu số!");
                                    }

                                    var title = worksheetQuestions.Cells[row, 4].Value?.ToString()?.Trim();
                                    MatchCollection matches = Regex.Matches(title, @"\(\.\.\.\)|\(\u2026\)");
                                    if (string.IsNullOrEmpty(title) || matches.Count >= 2)
                                    {
                                        throw new Exception($"Vui lòng xem lại trang tính {worksheetQuestions.Name}, dòng {row}, Ký tự '(...)' chỉ được xuất hiện 1 lần!");
                                    }

                                    int partIndex = int.Parse(worksheetQuestions.Cells[row, 1].Value?.ToString()?.Trim()) - 1;
                                    int groupIndex = int.Parse(worksheetQuestions.Cells[row, 2].Value?.ToString()?.Trim()) - 1;

                                    if (partIndex < 0 || groupIndex < 0)
                                    {
                                        throw new Exception($"Vui lòng xem lại trang tính {worksheetQuestions.Name}, dòng {row}, PartNo hoặc GroupNo không hợp lệ!");
                                    }

                                    var answers = worksheetQuestions.Cells[row, 5].Value?.ToString()?.Trim();
                                    var correctAnswer = worksheetQuestions.Cells[row, 6].Value?.ToString()?.Trim();

                                    if (string.IsNullOrEmpty(answers) || string.IsNullOrEmpty(correctAnswer)) throw new Exception($"Vui lòng xem lại trang tính {worksheetQuestions.Name}, dòng {row}, Đề thi chưa hoàn thiện!");
                                    parts[partIndex].Groups[groupIndex].Questions.Add(new Question()
                                    {
                                        QuestionNo = questionNo,
                                        Title = title,
                                        Answers = answers.Split("-").ToList(),
                                        CorrectAnswer = correctAnswer,
                                        ExplainString = worksheetQuestions.Cells[row, 7].Value?.ToString()?.Trim()
                                    });
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception($"Lỗi tại trang tính {worksheetQuestions.Name}, dòng {row}: {ex.Message}", ex);
                                }

                            }
                        }
                    }
                }
            }
            else
            {
                return null;
            }
            return parts;
        }

        public async Task<double> CalculateBandScore(int correctAnswers, string testType)
        {
            var bands = testType.ToLower() == "reading" ? ReadingBands : ListeningBands;

            foreach (var kvp in bands)
            {
                if (correctAnswers >= kvp.Key)
                {
                    return kvp.Value;
                }
            }
            return 0.0;
        }

        private static readonly Dictionary<int, double> ListeningBands = new Dictionary<int, double>
    {
        { 39, 9.0 }, { 37, 8.5 }, { 35, 8.0 }, { 32, 7.5 }, { 30, 7.0 },
        { 26, 6.5 }, { 23, 6.0 }, { 18, 5.5 }, { 16, 5.0 }, { 13, 4.5 },
        { 10, 4.0 }, { 7, 3.5 }, { 5, 3.0 }, { 3, 2.5 }, { 1, 2.0 }
    };

        private static readonly Dictionary<int, double> ReadingBands = new Dictionary<int, double>
    {
        { 39, 9.0 }, { 37, 8.5 }, { 35, 8.0 }, { 33, 7.5 }, { 30, 7.0 },
        { 27, 6.5 }, { 23, 6.0 }, { 19, 5.5 }, { 15, 5.0 }, { 13, 4.5 },
        { 10, 4.0 }, { 8, 3.5 }, { 6, 3.0 }, { 3, 2.5 }, { 1, 2.0 }
    };


        public async Task<double> CalculateOverall(double reading, double listening, double writing, double speaking)
        {
            double average = (listening + reading + writing + speaking) / 3;
            // Làm tròn đến 0.5 gần nhất
            double roundedBandScore = Math.Round(average * 2, MidpointRounding.AwayFromZero) / 2;

            return roundedBandScore;
        }

        public async Task UploadFileAudio(IFormFile? fileUploads, int questionBankId)
        {
            try
            {
                if (fileUploads != null)
                {
                    var fileName = "";
                    var ext = "";
                    var uploads = Path.Combine(_configuration["URL:BackendURL"], "uploads", "ielts", "audioes");

                    if (!Directory.Exists(uploads))
                        Directory.CreateDirectory(uploads);

                    var filePath = Path.Combine(uploads, fileUploads.FileName);
                    ext = Path.GetExtension(fileUploads.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await fileUploads.CopyToAsync(stream);
                        fileName = fileUploads.FileName;
                    }

                    var questionBank = await _context.QuestionBanks.FindAsync(questionBankId);
                    if (questionBank != null)
                    {
                        Listening listening = questionBank.ListeningJSON;
                        listening.ListeningFileURL = _configuration["URL:BackendURL"] + "/uploads/ielts/audioes/" + fileName;

                        questionBank.Listening = JsonSerializer.Serialize(listening);
                        _context.QuestionBanks.Update(questionBank);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected Error, please try again!");
            }
        }
    }
}
