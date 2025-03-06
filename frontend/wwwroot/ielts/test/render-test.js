// 1. Khai báo các biến và cấu hình chung
const API_BASE_URL = getCookie("apiurl") || "http://localhost:5000/api";
const EXAM_CANDIDATE_ID = new URL(window.location.href).searchParams.get('examcandidateid') || 0;
const QUESTIONBANK_ID = new URL(window.location.href).searchParams.get('questionbankid') || 0;


// 2. Các hàm tiện ích chung
function handleAjaxError(xhr, status, error) {
    console.error("AJAX Error:", error);
    console.error("Status:", status);
    console.error("Response:", xhr.responseText);
    //if (JSON.parse(xhr.responseText) != undefined) alert(JSON.parse(xhr.responseText).title);
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
    getExamcandidate: function (successCallback) {
        sendRequest("GET", `/ExamCandidates/${EXAM_CANDIDATE_ID}`, {}, successCallback);
    },

    getQuestionBank: function (successCallback) {
        sendRequest("GET", `/ExamCandidates/getByQuestionBankId/${QUESTIONBANK_ID}`, {}, successCallback);
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



// 4. Sự kiện liên quan đến giao diện (UI)
$(document).ready(function () {

    if (QUESTIONBANK_ID != 0 && EXAM_CANDIDATE_ID == 0) {
        ApiService.getQuestionBank(function (data) {
            render(data);
        });
    } else {
        ApiService.getExamcandidate(function (data) {
            render(data);
        });
    }
   

    function render(data) {
        $("#examcode").html("Exam Code: " + data.questionBank.examCode);
        var questions = $('div#questions');
        questions.html('');

        var result = '';
        var button = '';

        if (data.questionBank.readingJSON.parts) {
            result = data.submitedReading == '[]'
                && data.bandScoreReading == 0 ? `<i class="fas fa-times text-danger me-2"></i>Not Completed` : `<i class="fas fa-check-circle text-success me-2"></i>Completed`;
            button = data.submitedReading == '[]'
                && data.bandScoreReading == 0 ? `
                <a href="/ielts/test/reading?examcandidateid=${data.examCandidateId}" class="">
                    <button type="button" class="btn btn-dark mt-3">Start IELTS Online Reading</button>
                </a>` :
                `<a href="/ielts/review/reading?examcandidateid=${data.examCandidateId}" class="">
                    <button type="button" class="btn btn-info mt-3">Review</button>
                </a>`;
            questions.append(`
                <div class="card mb-5 border border-secondary" style="border-radius:20px;">
                    <div class="card-body">
                        <h2 class="card-title">IELTS Online Reading</h2>
                        <div class="my-2">
                            ${result}
                        </div>
                        <div class="text-dark my-2">
                            <i class="fas fa-star-half-alt me-2" style="color: #FFD43B;"></i>BandScore: ${data.bandScoreReading}
                        </div>
                        <div class="text-dark my-2">
                            <i class="far fa-clock me-2"></i>
                            Timing: ${data.questionBank.readingJSON.time / 60} minutes
                        </div>
                        ${button}
                    </div>
                </div>
            `);
        }

        if (data.questionBank.listeningJSON.parts) {
            result = data.submitedListening == '[]'
                && data.bandScoreListening == 0 ? `<i class="fas fa-times text-danger me-2"></i>Not Completed` : `<i class="fas fa-check-circle text-success me-2"></i>Completed`;
            button = data.submitedListening == '[]'
                && data.bandScoreListening == 0 ? `
                <a href="/ielts/test/listening?examcandidateid=${data.examCandidateId}" class="">
                    <button type="button" class="btn btn-dark mt-3">Start IELTS Online Listening</button>
                </a>` :
                `<a href="/ielts/review/listening?examcandidateid=${data.examCandidateId}" class="">
                    <button type="button" class="btn btn-info mt-3">Review</button>
                </a>`;
            questions.append(`
                <div class="card mb-5 border border-secondary" style="border-radius:20px;">
                    <div class="card-body">
                        <h2 class="card-title">IELTS Online Listening</h2>
                        <div class="text-dark my-2">
                            ${result}
                        </div>
                        <div class="text-dark my-2">
                            <i class="fas fa-star-half-alt me-2" style="color: #FFD43B;"></i>BandScore: ${data.bandScoreListening}
                        </div>
                        <div class="text-dark my-2">
                            <i class="far fa-clock me-2"></i>
                            Timing: ${data.questionBank.listeningJSON.time / 60} minutes
                        </div>
                        ${button}
                    </div>
                </div>
            `);

        }


        if (data.questionBank.writingJSON.parts) {
            result = data.submitedWriting == '[]'
                && data.bandScoreWriting == 0 ? `<i class="fas fa-times text-danger me-2"></i>Not Completed` : `<i class="fas fa-check-circle text-success me-2"></i>Completed`;
            button = data.submitedWriting == '[]'
                && data.bandScoreWriting == 0 ? `<button type="button" class="btn btn-dark mt-3">Start IELTS Online Writing</button>` : ``;
            questions.append(`
                <div class="card mb-5 border border-secondary" style="border-radius:20px;">
                    <div class="card-body">
                        <h2 class="card-title">IELTS Online Writing</h2>
                        <div class="text-dark my-2">
                            ${result}
                        </div>
                        <div class="text-dark my-2">
                            <i class="fas fa-star-half-alt me-2" style="color: #FFD43B;"></i>BandScore: ${data.bandScoreWriting}
                        </div>
                        <div class="text-dark my-2">
                            <i class="far fa-clock me-2"></i>
                            Timing: ${data.questionBank.writingJSON.time / 60} minutes
                        </div>
                        <a href="/ielts/test/writing?examcandidateid=${data.examCandidateId}" class="">
                            ${button}
                        </a>
                    </div>
                </div>
            `);
        }

    }

    
});

