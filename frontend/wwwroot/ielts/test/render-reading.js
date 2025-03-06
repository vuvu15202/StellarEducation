// 1. Khai báo các biến và cấu hình chung
const API_BASE_URL = getCookie("apiurl") || "http://localhost:5000/api";
const EXAM_CANDIDATE_ID =
  new URL(window.location.href).searchParams.get("examcandidateid") || 0;
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
  var cookies = document.cookie.split(";");
  // Loop through each cookie
  for (var i = 0; i < cookies.length; i++) {
    var cookie = cookies[i];
    // Trim leading and trailing spaces
    cookie = cookie.trim();
    // Check if this cookie is the one we're looking for
    if (cookie.startsWith(name + "=")) {
      // Return the cookie value
      return cookie.substring(name.length + 1);
    }
  }
  // Return null if cookie not found
  return null;
}
function sendRequest(
  method,
  endpoint,
  data = {},
  successCallback,
  errorCallback = handleAjaxError
) {
  var token = getCookie("token");
  $.ajax({
    url: `${decodeURIComponent(API_BASE_URL)}${endpoint}`,
    method: method,
    contentType: "application/json",
    headers: {
      Authorization: `Bearer ${token}`,
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
    sendRequest(
      "GET",
      `/ExamCandidates/${EXAM_CANDIDATE_ID}/reading`,
      {},
      successCallback
    );
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
  $("i.fa-check").hide();
  let countdownTime = 0;
  var timer;

  ApiService.getExamination(function (data) {
    const tabs = $("#pills-tab");
    const tabcontent = $("#pills-tabContent");
    QUESTION_BANK_ID = data.questionBankId;

    tabs.empty();
    tabcontent.empty();

    //render thời gian
    countdownTime = data.readingJSON.time;
    updateCountdown();

    data.readingJSON.parts.forEach(function (part) {
      tabs.append(`
                <li class="nav-item" role="presentation">
                    <button class="nav-link" id="pills-${part.partNo}-tab" data-bs-toggle="pill" data-bs-target="#pills-${part.partNo}" type="button" role="tab" aria-controls="pills-${part.partNo}" aria-selected="true">Part ${part.partNo}</button>
                </li>
            `);

      var fileurl = "";
        if (part.fileType == "jpg" || part.fileType == "png" || part.fileType == "jpeg")
        fileurl = `<img src="${part.fileURL}" width="100%;" />`;
      else if (part.fileType == "pdf")
          fileurl = `<embed class="" src="${part.fileURL}#toolbar=0" style="border: 0px;" width="100%;" height="100%;" />`;

      tabcontent.append(`
                <div class="tab-pane fade show" id="pills-${part.partNo}" role="tabpanel" aria-labelledby="pills-${part.partNo}-tab">
                    <div class="row justify-content-center" style="height:65vh; box-sizing:border-box; ">
                        <div class="col-md-5 mx-4 border border-dark px-0 rounded h-100" style="overflow-y:scroll;">
                            ${fileurl}
                        </div>
                        <div class="col-md-5 mx-4 border border-dark px-0 rounded">
                            <div id="scrollspy-part${part.partNo}" class="scrollspy-example px-3 bg-secondary bg-opacity-10" style="overflow-y:scroll; height:65vh;" data-bs-spy="scroll" data-bs-target="#list-example" data-bs-offset="0" tabindex="0">
                    
                            </div>
                        </div>
                    </div>
                    <nav aria-label="Page navigation example">
                        <ul id="scrollspyitem-part${part.partNo}" class="pagination justify-content-center my-3">
                            <li class="page-item">
                                <a class="list-group-item list-group-item-action bg-white text-dark" href="#list-item-1">1<i class="fas fa-check text-success ms-1"></i></a>
                            </li>
                        </ul>
                    </nav>
                </div>
            `);
      tabcontent.children().first().addClass("active");
      renderGroups(part);
    });
    tabs.children().first().find("button").addClass("active");

    $('div[role="tabpanel"]').css({ opacity: "0.1" });
    $(document).trigger("contentRendered");
  });

  function formatTime(seconds) {
    const minutes = Math.floor(seconds / 60);
    const remainingSeconds = seconds % 60;
    return `${minutes}:${remainingSeconds.toString().padStart(2, "0")}`;
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
        answers.push(...getInputRadioElement());
        answers.push(...getInputCheckboxElement());
        answers.push(...getInputTextElement());
        answers.push(...getInputDropboxElement());
        console.log(answers);
        document.getElementById("lockScreen").classList.add("active"); // Kích hoạt khóa màn hình
        var submitedAnswer = {
            questionBankId: QUESTION_BANK_ID,
            examCode: "sdbhjsd",
            answers: answers,
            forQuestion: "reading",
        };
        ApiService.gradeExamination(submitedAnswer, function (data) {
            console.log("result = " + data);
            alert(data);
            //window.location.href = `/ielts/test?examcandidateid=${EXAM_CANDIDATE_ID}`;
            window.location.href = `/ielts/test?questionbankid=${QUESTION_BANK_ID}`;
        });
    }

  $("#btn-start").click(() => {
    $('div[role="tabpanel"]').css({ opacity: "1" });
      timer = setInterval(updateCountdown, 1000);
      $("#btn-start").prop("disabled", true);
      $("#btn-submit1").removeClass("d-none");
  });

  $(document).on(
    "click",
    'input[type="radio"] , input[type="checkbox"]',
    function () {
      if ($(this).prop("checked")) {
        var questionno = $(this).data("questionno");
        $(`i[data-completeiconid="${questionno}"]`).removeClass("d-none");
      } else {
        var questionno = $(this).data("questionno");
        $(`i[data-completeiconid="${questionno}"]`).addClass("d-none");
      }
    }
  );
  $(document).on("input", 'input[type="text"]', function () {
    if ($(this).val() != "") {
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

  function getInputRadioElement() {
    const inputs = $(
      'div[id^="group"] input[type="radio"][name^="answer"]:checked'
    );
    var answers = [];
    inputs.each(function () {
      answers.push({
        questionNo: $(this).data("questionno") + "",
        submitedAnswer: $(this).attr("value"),
      });
    });
    return answers;
  }
  function getInputCheckboxElement() {
    const rows = $('div[id^="group"][data-type="checkbox"] div[class="row"]');
    var answers = [];
    rows.each(function () {
      answers.push({
        questionNo: $(this).data("questionno") + "",
        submitedAnswer: getSelectedCheckboxAnswers(
          $(this).attr("data-questionno")
        ),
      });
    });
    return answers;
  }
  function getSelectedCheckboxAnswers(questionId) {
    // Lấy tất cả các checkbox được check trong câu hỏi có ID tương ứng
    const checkedInputs = $(
      `div[data-questionno="${questionId}"] input[type="checkbox"]:checked`
    );

    // Lấy giá trị các checkbox được check và nối thành chuỗi
    return checkedInputs
      .map(function () {
        return $(this).val();
      })
      .get()
      .join("-");
  }
  function getInputTextElement() {
    const inputs = $('div[id^="group"] input[type="text"][name^="answer"]');
    var answers = [];
    inputs.each(function () {
      answers.push({
        questionNo: $(this).data("questionno") + "",
        submitedAnswer: $(this).val(),
      });
    });
    return answers;
  }
  function getInputDropboxElement() {
    var answers = [];
    $(".dropbox").each(function () {
      answers.push({
        questionNo: $(this).data("questionno") + "",
        submitedAnswer:
          $(this).data("dropped-answer") == undefined
            ? ""
            : $(this).data("dropped-answer"),
      });
    });
    return answers;
  }
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

function renderGroups(part) {
  var scrollspy = $(`#scrollspy-part${part.partNo}`);
  var scrollspyitem = $(`#scrollspyitem-part${part.partNo}`);
  scrollspy.empty();
  scrollspyitem.empty();
  var groups = part.groups;

  groups.forEach(function (group) {
    scrollspy.append(`
            <div class="mb-3 fw-bold">Questions ${group.questionRange}</div>
            <div class="mb-3">${group.title}</div>
    `);
    scrollspy.append(`
        <div id="group-part${part.partNo}-group${group.groupNo}" data-type="${group.type}" class="card mb-5"></div>
    `);
    if (group.type == "dropbox") {
      var questions = `
        <div id="list-item" class="card-body bg-white pt-3">
            <div class="row">
                <div class="col-8 pb-2">
                    <div class="question">
                        (title)
                    </div>
                </div>
                <div class="col-4 pb-2 answers">
                    (answers)
                </div>
            </div>
        </div>`;
      var title = "";
      var answers = "";
      group.questions.forEach(function (question) {
        scrollspyitem.append(`
            <li class="page-item">
                <a class="list-group-item list-group-item-action bg-white text-dark" href="#list-item-${question.questionNo}">${question.questionNo}<i data-completeiconid="${question.questionNo}" class="fas fa-check text-success ms-1 d-none fa-xs"></i></a>
            </li>
        `);
        title +=
          `<span id="list-item-${question.questionNo}" class="fw-bold me-3">${question.questionNo}</span>` +
          question.title.replace(
            /\(\.\.\.\)|\(\u2026\)/g,
            `<span data-questionno="${question.questionNo}" name="answer-part${part.partNo}-group${group.groupNo}-question${question.questionNo}" class="dropbox fw-bold">${question.questionNo}</span>`
          ) +
          "</br></br>";
        answers += $.map(question.answers, function (answer) {
          return `<div class="answer" data-answer="${answer}">${answer}</div>`;
        }).join("");
        console.log(answers);
      });

      $(`#group-part${part.partNo}-group${group.groupNo}`).append(
        questions.replace(`(title)`, title).replace(`(answers)`, answers)
      );
    } else {
      var questions = $.map(group.questions, function (question) {
        scrollspyitem.append(`
        <li class="page-item">
            <a class="list-group-item list-group-item-action bg-white text-dark" href="#list-item-${question.questionNo}">${question.questionNo}<i data-completeiconid="${question.questionNo}" class="fas fa-check text-success ms-1 d-none fa-xs"></i></a>
        </li>
    `);

        if (group.type == "radio") {
          var answer = $.map(question.answers, function (answer) {
            return `<div class="col-12 pb-2">
                    <input type="radio" name="answer-part${part.partNo}-group${group.groupNo}-question${question.questionNo}"  data-questionno="${question.questionNo}" value="${answer}" class="card-text answer text-success" /> <label class="ms-1">${answer}</label>
                </div>
                `;
          });
          return `
        <div class="card">
            <div id="list-item-${question.questionNo}" class="card-body">
                <div class="card-title fw-bold">${question.questionNo}<span class="ms-3 fw-normal">${question.title}</span></div>
                <div class="row">
                    (answer)
                </div>
            </div>
        </div>
    `.replace(`(answer)`, answer.join(""));
        } else if (group.type == "checkbox") {
          var answer = $.map(question.answers, function (answer) {
            return `<div class="col-12 pb-2">
                    <input type="checkbox" name="answer-part${part.partNo}-group${group.groupNo}-question${question.questionNo}" data-questionno="${question.questionNo}" value="${answer}" class="card-text answer text-success" /> <label class="ms-1">${answer}</label>
                </div>
                `;
          });
          return `
        <div class="card">
            <div id="list-item-${question.questionNo}" class="card-body">
                <div class="card-title fw-bold">${question.questionNo}<span class="ms-3 fw-normal">${question.title}</span></div>
                <div data-questionno="${question.questionNo}" class="row">
                    (answer)
                </div>
            </div>
        </div>
    `.replace(`(answer)`, answer.join(""));
        } else if (group.type == "text") {
          var title = question.title.replace(
            /\(\.\.\.\)|\(\u2026\)/g,
            `<input data-questionno="${question.questionNo}" type="text" name="answer-part${part.partNo}-group${group.groupNo}-question${question.questionNo}" class="">`
          );
          return `
                <div class="card">
                    <div id="list-item-${question.questionNo}" class="card-body">
                        <div class="row">
                            <div class="col-12 pb-2">
                                <div class="">
                                    <span class="fw-bold me-3">${question.questionNo} </span>
                                    (title)
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            `.replace(`(title)`, title);
        }
      });
      $(`#group-part${part.partNo}-group${group.groupNo}`).append(
        questions.join("")
      );
    }
  });
  scrollspy.append(` 
        <div class="card mb-3">
            <div class="card-body">
                <h5 class="card-title text-center">THE END</h5>

            </div>
        </div>
    `);
  $(document).trigger("contentRendered");
}
