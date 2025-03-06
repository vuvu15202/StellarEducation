// 1. Khai báo các biến và cấu hình chung
const API_BASE_URL = getCookie("apiurl") || "http://localhost:5000/api";
const QUESTION_BANK_ID = new URL(window.location.href).searchParams.get('questionbankid') || 0;
const EXAM_CANDIDATE_ID = new URL(window.location.href).searchParams.get('examcandidateid') || 0;


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
    getExamination: function (successCallback) {
        sendRequest("GET", `/QuestionBanks/editquestion/${QUESTION_BANK_ID}/writing`, {}, successCallback);
    },

    // Chấm điểm
    gradeExamination: function (answers, successCallback) {
        sendRequest("POST", "/QuestionBanks/GradeAdmin", answers, successCallback);
    },

    // Cập nhật dữ liệu
    getAnswers: function (successCallback) {
        sendRequest("GET", `/QuestionBanks/getAnswersWriting/${EXAM_CANDIDATE_ID}`, {}, successCallback);
    },

    // Xóa dữ liệu
    GradeWriting: function (grade, successCallback) {
        sendRequest("POST", `/QuestionBanks/gradeAnswersWriting/${EXAM_CANDIDATE_ID}`, grade, successCallback);
    },
};



// 4. Sự kiện liên quan đến giao diện (UI)
$(document).ready(function () {
    $('i.fa-check').hide();

    ApiService.getExamination(function (data) {
        const tabs = $("#pills-tab");
        const tabcontent = $("#pills-tabContent");

        tabs.empty();
        tabcontent.empty();


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
                            <input data-partno="${part.partNo}" class="btn btn-muted file-picture" type="file" name="file" accept=".jpg, .jpeg, .png, .pdf" />
                            <button class="btn btn-info save-picture">Save Picture</button>
                            <div class="picture h-75">
                                ${fileurl}
                            </div>
                        </div>
                        <div class="col-md-5 mx-4 px-0 rounded">
                            <textarea data-questionno="${part.partNo}" id="answer-part${part.partNo}" class="input-area border border-dark w-100 rounded h-100 p-3" rows="20" placeholder="Text here..."></textarea>
                        </div>
                </div>
            `);
            tabcontent.children().first().addClass("active");
        });
        tabs.children().first().find('button').addClass("active");

        document.getElementById("countdown").textContent = formatTime(data.writingJSON.time);

        if (EXAM_CANDIDATE_ID != 0) {
            renderAnswers();
        }

        $(document).trigger("contentRendered");
    });
    function renderAnswers() {
        ApiService.getAnswers(function (data) {
            var offcanvasGradeWriting = $(`div[id="offcanvasGradeWriting"]`);
            $.each(JSON.parse(data.answers), function (index, answer) {
                $(`textarea[id^="answer-part${answer.QuestionNo}"]`).val(answer.SubmitedAnswer ? answer.SubmitedAnswer : "Not Submited");
                offcanvasGradeWriting.append(`
                    <div class="col-md-12 row justify-content-around">
                        <div class="col-md-5">
                            <label class="fw-bold">Part ${index+1}:</label>
                        </div>
                        <div class="col-md-6">
                            <input class="form-control mb-4" type="number" min="0" max="9" step="0.5" name="grade-part${index + 1}" id="grade-part${index+1}" />
                        </div>
                    </div>
                `);
            });

            offcanvasGradeWriting.append(`
                <hr />
                <button id="gradeWritingBtn" class="btn btn-info">Grade</button>
            `)

        });
    }

    $(document).on("click", "#gradeWritingBtn", function () {
        const inputs = $(
            'input[id^="grade-part"]'
        );
        var answers = [];
        inputs.each(function () {
            answers.push(parseFloat($(this).val()));
        });
        ApiService.GradeWriting(answers, function (data) {
            alert(data);
        });

    });

    function formatTime(seconds) {
        const minutes = Math.floor(seconds / 60);
        const remainingSeconds = seconds % 60;
        return `${minutes}:${remainingSeconds.toString().padStart(2, '0')}`;
    }

    $(document).on("change", "input.file-picture", function () {
        const file = this.files[0]; // Lấy file từ DOM element
        if (!file) return; // Không làm gì nếu không có file

        const allowedExtensions = ["jpg", "jpeg", "png", "pdf"];
        const fileExtension = file.name.split('.').pop().toLowerCase();

        if (!allowedExtensions.includes(fileExtension)) {
            alert("Chỉ chấp nhận ảnh định dạng .jpg, .jpeg, .png hoặc .pdf!");
            $(this).val(""); // Reset input
            return;
        }

        const reader = new FileReader();

        // Khi file được đọc xong
        reader.onload = (e) => {
            const parentDiv = $(this).closest("div"); // Tìm thẻ div chứa class file-picture
            const imgElement = parentDiv.find("div.picture"); // Tìm thẻ img trong div đó
            if (["jpg", "jpeg", "png"].includes(fileExtension)) imgElement.html(`<img src="${e.target.result}" width="100%;" />`);
            else imgElement.html(`<embed class="" src="${e.target.result}" style="border: 0px;" width="100%;" height="100%;" />`);
            //imgElement.attr("src", e.target.result).attr("hidden", false); // Hiển thị ảnh
        };

        reader.readAsDataURL(file); // Đọc file dưới dạng Data URL
    });

    $(document).on("click", "button.save-picture", function () {
        const parentDiv = $(this).closest("div"); // Tìm thẻ div chứa class file-picture
        const inputEle = parentDiv.find("input.file-picture"); // Tìm thẻ img trong div đó
        var partNo = inputEle.data("partno");
        const image = inputEle.prop('files')[0];

        const formData = new FormData();
        formData.append('questionBankId', QUESTION_BANK_ID);
        formData.append('partNo', partNo);
        formData.append('forQuestion', 'writing');
        formData.append('fileUploads', image);

        $.ajax({
            url: `${decodeURIComponent(API_BASE_URL)}/QuestionBanks/UploadPicture`,
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                alert("Successful!");
                location.reload();
            },
            error: function (error) {
                console.log('Error:', error);
            }
        });
    });

    $(document).on("click", "button#btn-file-excel", function () {
        var inputExcel = $(`input#fileExcel`); console.log(inputExcel)
        var time = $(`#time`).val();

        const formData = new FormData();
        formData.append('questionBankId', QUESTION_BANK_ID);
        formData.append('forQuestion', 'writing');
        formData.append('time', time);
        formData.append('fileUploads', inputExcel.prop('files')[0]);

        $.ajax({
            url: `${decodeURIComponent(API_BASE_URL)}/QuestionBanks/UploadQuestionExcel`,
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                alert("Successful!");
                location.reload();
            },
            error: function (xhr, status, error) {
                console.log("Error:", error);
                alert(xhr.responseText);
            },
        });
    });
    

    $(document).on("click", "#btnSubmit", function () {
        //const productId = $(this).data("id");
        if (confirm("Bạn có chắc muốn nộp bài?")) {
            var answers = [];
            answers.push(...getInputtextAreaElement());
            console.log(answers);
            var submitedAnswer = {
                questionBankId: QUESTION_BANK_ID,
                forQuestion: "writing",
                examCode : "sdbhjsd",
                answers : answers
            };
            ApiService.gradeExamination(submitedAnswer, function (data) {
                console.log("result = " + data);
            });
        }
    });

    $(document).on("click", 'div#downloadexcel', function () {
        var token = getCookie('token');
        $.ajax({
            url: `${decodeURIComponent(API_BASE_URL)}/QuestionBanks/template/download/writing`,
            type: "get",
            headers: {
                "Authorization": "Bearer " + token,
            },
            // data: JSON.stringify(requestData),
            contentType: "application/json",
            xhrFields: {
                responseType: 'blob' // Đặt kiểu phản hồi là blob
            },
            success: function (blob, status, xhr) {
                // Tạo URL từ Blob
                var downloadUrl = URL.createObjectURL(blob);
                // Tạo một thẻ a để tải về file
                var a = document.createElement("a");
                a.href = downloadUrl;
                a.download = "templateWriting.xlsx"; // Tên file bạn muốn tải về
                document.body.appendChild(a);
                a.click();
                document.body.removeChild(a);

                // Giải phóng bộ nhớ
                URL.revokeObjectURL(downloadUrl);
            },
            error: function (xhr, status, error) {
                console.log(xhr)
            }
        });

    });
    function getInputtextAreaElement() {
        const inputs = $('textarea[id^="answer"]');
        var answers = [];
        inputs.each(function () {
            answers.push(
                {
                    questionNo: $(this).data('questionno'),
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

