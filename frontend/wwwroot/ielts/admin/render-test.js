// 1. Khai báo các biến và cấu hình chung
const API_BASE_URL = getCookie("apiurl") || "http://localhost:5000/api";
const QUESTION_BANK_ID = new URL(window.location.href).searchParams.get('questionbankid') || 0;

// 2. Các hàm tiện ích chung
function handleAjaxError(xhr, status, error) {
    console.error("AJAX Error:", error);
    console.error("Status:", status);
    console.error("Response:", xhr.responseText);
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
    getQuestionBank: function (successCallback) {
        sendRequest("GET", `/QuestionBanks/${QUESTION_BANK_ID}`, {}, successCallback);
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
    
    ApiService.getQuestionBank(function (data) {
        $("#examcode").html("Exam Code: " + data.examCode);
        var questions = $('div#questions');
        questions.html('');
        questions.append(`
            <div class="card mb-5 border border-secondary" style="border-radius:20px;">
                <div class="card-body">
                    <h2 class="card-title">IELTS Online Reading</h2>
                    <div class="text-dark my-2"><i class="far fa-clock me-2"></i>Timing: ${data.readingJSON.time / 60} Hour</div>
                    <a href="/admin/questionbankEdit/reading?questionbankid=${QUESTION_BANK_ID}" class="">
                        <button type="button" class="btn btn-dark mt-3">Edit</button>
                    </a>
                </div>
            </div>
        `);

        questions.append(`
            <div class="card mb-5 border border-secondary" style="border-radius:20px;">
                <div class="card-body">
                    <h2 class="card-title">IELTS Online Listening</h2>
                    <div class="text-dark my-2"><i class="far fa-clock me-2"></i>Timing: ${data.listeningJSON.time/60} Hour</div>
                    <a href="/admin/questionbankEdit/listening?questionbankid=${QUESTION_BANK_ID}" class="">
                        <button type="button" class="btn btn-dark mt-3">Edit</button>
                    </a>
                </div>
            </div>
        `);

        questions.append(`
            <div class="card mb-5 border border-secondary" style="border-radius:20px;">
                <div class="card-body">
                    <h2 class="card-title">IELTS Online Writing</h2>
                    <div class="text-dark my-2"><i class="far fa-clock me-2"></i>Timing: ${data.writingJSON.time/60} Hour</div>
                    <a href="/admin/questionbankEdit/writing?questionbankid=${QUESTION_BANK_ID}" class="">
                        <button type="button" class="btn btn-dark mt-3">Edit</button>
                    </a>
                </div>
            </div>
        `);
    });

    
});

