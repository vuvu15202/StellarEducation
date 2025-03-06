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
    getExamination: function (successCallback) {
        sendRequest("GET", `/ExamCandidates/${EXAM_CANDIDATE_ID}/writing`, {}, successCallback);
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
    $('i.fa-check').hide();
    var countdownTime;
    var timer;

    ApiService.getExamination(function (data) {
        const tabs = $("#pills-tab");
        const tabcontent = $("#pills-tabContent");
        QUESTION_BANK_ID = data.questionBankId;

        tabs.empty();
        tabcontent.empty();

        //render thời gian
        countdownTime = data.writingJSON.time;
        updateCountdown();

        data.writingJSON.parts.forEach(function (part) {
            tabs.append(`
                <li class="nav-item" role="presentation">
                    <button class="nav-link" id="pills-${part.partNo}-tab" data-bs-toggle="pill" data-bs-target="#pills-${part.partNo}" type="button" role="tab" aria-controls="pills-${part.partNo}" aria-selected="true">Part ${part.partNo}</button>
                </li>
            `);
            var fileurl = '';
            if (part.fileType == "jpg" || part.fileType == "png" || part.fileType == "jpeg") fileurl = `<img src="${part.fileURL}" width="100%;" />`;
            else if (part.fileType == 'pdf') fileurl = `<embed class="" src="${part.fileURL}#toolbar=0" style="border: 0px;" width="100%;" height="100%;" />`;

            tabcontent.append(`
                <div class="tab-pane fade show" id="pills-${part.partNo}" role="tabpanel" aria-labelledby="pills-${part.partNo}-tab">
                    <div class="row justify-content-center" style="height:65vh; box-sizing:border-box;">
                        <div class="col-md-5 mx-4 border border-dark px-0 rounded h-100" style="overflow-y:scroll;"">
                            ${fileurl}
                        </div>
                        <div class="col-md-5 mx-4 px-0 rounded">
                            <textarea data-questionno="${part.partNo}" id="answer-part${part.partNo}" class="input-area border border-dark w-100 rounded h-100 p-3" rows="20" placeholder="Text here..."></textarea>
                        </div>
                </div>
            `);
            tabcontent.children().first().addClass("active");
        });
        tabs.children().first().find('button').addClass("active");

        $('div[role="tabpanel"]').css({ 'opacity': '0.1' });
        $(document).trigger("contentRendered");
    });

    function formatTime(seconds) {
        const minutes = Math.floor(seconds / 60);
        const remainingSeconds = seconds % 60;
        return `${minutes}:${remainingSeconds.toString().padStart(2, '0')}`;
    }

    function updateCountdown() {
        const countdownElement = document.getElementById("countdown");

        if (countdownTime >= 0) {
            countdownElement.textContent = formatTime(countdownTime); // Cập nhật giao diện

            if (countdownTime <= 60) {
                countdownElement.classList.add("text-danger"); // Thêm class để đổi màu chữ
                countdownElement.classList.add("zoom-text"); // Thêm class để đổi màu chữ
            }

            countdownTime--;
        } else {
            clearInterval(timer); // Dừng bộ đếm
            countdownElement.textContent = "Time's up!";
            document.getElementById("lockScreen").classList.add("active"); // Kích hoạt khóa màn hình
            submit();
        }
    }

    $(document).on("click", "#btnSubmit", function () {
        submit();
    });

    function submit() {
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

    $("#btn-start").click(() => {
        $('div[role="tabpanel"]').css({ 'opacity': '1' });
        timer = setInterval(updateCountdown, 1000);
        $("#btn-start").prop("disabled", true);
        $("#btn-submit1").removeClass("d-none");
    });

    function getInputtextAreaElement() {
        const inputs = $('textarea[id^="answer"]');
        var answers = [];
        inputs.each(function () {
            answers.push(
                {
                    questionNo: $(this).data('questionno') + '',
                    submitedAnswer: $(this).val()
                }
            );
        });
        return answers;
    }

    $(document).on("click", 'input[type="radio"] , input[type="checkbox"]', function () {
        if ($(this).prop('checked')) {
            var questionno = $(this).data("questionno");
            $(`i[data-completeiconid="${questionno}"]`).removeClass("d-none");
        } else {
            var questionno = $(this).data("questionno");
            $(`i[data-completeiconid="${questionno}"]`).addClass("d-none");
        }
    });
    $(document).on("input", 'input[type="text"]', function () {
        if ($(this).val() != '') {
            var questionno = $(this).data("questionno");
            $(`i[data-completeiconid="${questionno}"]`).removeClass("d-none");
        } else {
            var questionno = $(this).data("questionno");
            $(`i[data-completeiconid="${questionno}"]`).addClass("d-none");
        }
    });

    //khong ap dung cho phan tu dong
    //$('input[type="radio"]').click(function () {

    //});

    
   


});


//var newDiv = $('<div></div>')
//    .text('abc') // Nội dung là "abc"
//    .addClass('my-class') // Thêm class "my-class"
//    .attr('data-id', '1') // Thêm thuộc tính data-id="1"
//    .css({ // Áp dụng CSS
//        'background-color': 'lightblue',
//        'padding': '10px',
//        'border': '1px solid #ccc'
//    });

//$(document).on("click", "#btnSubmit", function () {
//    //const productId = $(this).data("id");
//    if (confirm("Bạn có chắc muốn xóa sản phẩm này?")) {
//        ApiService.deleteProduct(productId, function () {
//            alert("Xóa sản phẩm thành công!");
//            $("#loadProductsBtn").click(); // Tải lại danh sách
//        });
//    }
//});

