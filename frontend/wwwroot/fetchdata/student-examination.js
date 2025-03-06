// 1. Khai báo các biến và cấu hình chung
const API_BASE_URL = getCookie("apiurl") || "http://localhost:5000/api";
const EXAM_CANDIDATE_ID = new URL(window.location.href).searchParams.get('examcandidateid') || 0;
var QUESTION_BANK_ID = 0;

// 2. Các hàm tiện ích chung
function handleAjaxError(xhr, status, error) {
    console.error("AJAX Error:", error);
    console.error("Status:", status);
    console.error("Response:", xhr.responseText);
    //alert("Có lỗi xảy ra, vui lòng thử lại sau.");
    alert(xhr.responseText);
}
function getCookie(name) {
    // Split cookies by semicolon
    var cookies = document.cookie.split(';');
    // Loop through each cookie
    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i];
        // Trim leading and trailing spaces
        cookie = cookie.trim();
        // Check if this cookie is the one we're looking for
        if (cookie.startsWith(name + '=')) {
            // Return the cookie value
            return cookie.substring(name.length + 1);
        }
    }
    // Return null if cookie not found
    return null;
}
function sendRequest(method, endpoint, data = {}, successCallback, errorCallback = handleAjaxError) {
    var token = getCookie('token');
    $.ajax({
        url: `${decodeURIComponent(API_BASE_URL)}${endpoint}`,
        method: method,
        contentType: "application/json",
        headers: {
            Authorization: `Bearer ${token}`
        },
        data: method === "GET" ? data : JSON.stringify(data),
        success: successCallback,
        error: errorCallback,
    });
}

// 3. Các hàm API cụ thể
const ApiService = {
    // Lấy danh sách dữ liệu (ví dụ: sản phẩm)
    getExaminations: function (successCallback) {
        sendRequest("GET", `/ExamCandidates/bystudentid`, {}, successCallback);
    },

    // Chấm điểm
    gradeExamination: function (answers, successCallback) {
        sendRequest("POST", "/QuestionBanks/Grade", answers, successCallback);
    },

    // Cập nhật dữ liệu
    updateProduct: function (productId, productData, successCallback) {
        sendRequest("PUT", `/products/${productId}`, productData, successCallback);
    },

    // Xóa dữ liệu
    deleteProduct: function (productId, successCallback) {
        sendRequest("DELETE", `/products/${productId}`, {}, successCallback);
    },
};

var examsGlobal = [];

// 4. Sự kiện liên quan đến giao diện (UI)
$(document).ready(function () {

    ApiService.getExaminations(function (data) {
        examsGlobal = data;
        $("examinations").html("");
        $.each(data, function (index, exam) {
            var completeBadge = exam.isComplete ? `<td class="text-center"><span class="badge badge-sm bg-gradient-success">Completed</span></td>` : `<td class="text-center"><span class="badge badge-sm bg-gradient-secondary">Not Completed</span></td>`;
            $("#examinations").append(`
                <tr>
                    <td>${index + 1}</td>
                    <td><a href="javascript:void(0)" class="examcandidate" data-examcandidateid="${exam.isComplete}">${exam.questionBank.examCode}</a></td>
                    <td>${exam.questionBank.startDate}</td>
                    <td>${exam.questionBank.endDate}</td>
                    ${completeBadge}
                    <td class="">
                        <div>
                            <button class="btn btn-light" type="button" id="ellipsisMenu" data-bs-toggle="dropdown" aria-expanded="false">
                                <i class="fas fa-ellipsis-v"></i>
                            </button>
                            <ul class="dropdown-menu" aria-labelledby="ellipsisMenu">
                                <li>
                                    <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4 examcandidate" data-examcandidateid="${exam.isComplete}">
                                        <i class="fas fa-file-pdf text-lg me-1"></i>
                                        Xem chi tiết
                                    </button>
                                    </hr>
                                </li>
                                <li>
                                    <a href="/ielts/test?examcandidateid=${exam.examCandidateId}" target="_blank">
                                        <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4 viewCandidates">
                                            <i class="fas fa-edit fa-lg text-lg me-1" style="color: #7d00b8;"></i>
                                            Vào thi
                                        </button>
                                    </a>
                                    </hr>
                                </li>
                            </ul>
                        </div>
                    </td>
                </tr>
            `);
            
        });
    });
    $(document).on("click", "a.examcandidate,button.examcandidate", function () {
        const examcandidateid = $(this).data("examcandidateid");
        var examcandidate = examsGlobal.find(e => e.examCandidateId == examcandidateid);
        $("#exam-detail").html("");
        if (examcandidate) {
            $("#exam-detail").append(`
                 <div class="card-body">
                    <h4 class="card-title mb-4">${examcandidate.questionBank.examCode}</h4>
                    <p class="card-text"><strong>Start Exam at:</strong> ${examcandidate.questionBank.startDate}</p>
                    <p class="card-text"><strong>Complete at:</strong> ${examcandidate.questionBank.endDate}</p>
                    <p class="card-text"><strong>BandScores:</strong></p>
                    <ul>
                        <li>Reading: ${examcandidate.bandScoreReading}</li>
                        <li>Listening: ${examcandidate.bandScoreListening}</li>
                        <li>Writing: ${examcandidate.bandScoreWriting}</li>
                    </ul>
                    <p class="card-text"><strong>Overall:</strong> ${examcandidate.overall}</p>
                </div>
            `);
            $('#examinations-col').css('width', '65%');
            $('#examinations-col').css('float', 'left');
            $('#examinationdetail-col').css('width', '35%');
            $('#examinationdetail-col').css('float', 'right');
            $('#examinationdetail-col').css('display', 'block');
        }
    });

    $(document).on("click", "#btnSubmit", function () {
        //const productId = $(this).data("id");
        if (confirm("Bạn có chắc muốn nộp bài?")) {
            var answers = [];
            answers.push(...getInputtextAreaElement());
            console.log(answers);
            document.getElementById("lockScreen").classList.add("active"); // Kích hoạt khóa màn hình
            var submitedAnswer = {
                questionBankId: QUESTION_BANK_ID,
                examCode: "sdbhjsd",
                answers: answers,
                forQuestion: "writing"
            };
            ApiService.gradeExamination(submitedAnswer, function (data) {
                console.log("result = " + data);
                alert(data);
                window.location.href = `/ielts/test?examcandidateid=${EXAM_CANDIDATE_ID}`;
            });
        }
    });
});
