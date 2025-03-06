// 1. Khai báo các biến và cấu hình chung
const API_BASE_URL = getCookie("apiurl") || "http://localhost:5000/api";
const QUESTION_BANK_ID = new URL(window.location.href).searchParams.get("questionbankid") || 0;
console.log(window.location.href);

// 2. Các hàm tiện ích chung
function handleAjaxError(xhr, status, error) {
  console.error("AJAX Error:", error);
  console.error("Status:", status);
  console.error("Response:", xhr.responseText);
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
      `/QuestionBanks/editquestion/${QUESTION_BANK_ID}/listening`,
      {},
      successCallback
    );
  },

  // Chấm điểm
  gradeExamination: function (answers, successCallback) {
    sendRequest("POST", "/QuestionBanks/GradeAdmin", answers, successCallback);
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
  let countdownTime = 0; // Tổng thời gian (giây)
  var $audio = $("#audioPlayer");
  const $progressBar = $("#progressBar");
  const $currentTime = $("#currentTime");
  const $duration = $("#duration");

  ApiService.getExamination(function (data) {
    const tabs = $("#pills-tab");
    const tabcontent = $("#pills-tabContent");

    tabs.empty();
    tabcontent.empty();

    countdownTime = data.listeningJSON.time;
    document.getElementById("countdown").textContent =
      formatTimeCountdown(countdownTime);

    //tải src âm thanh
    $("#listeningFileURL").attr("src", data.listeningJSON.listeningFileURL);
    loadAudio(data.listeningJSON.listeningFileURL);
    data.listeningJSON.parts.forEach(function (part) {
      tabs.append(`
                <li class="nav-item" role="presentation">
                    <button class="nav-link" id="pills-${part.partNo}-tab" data-bs-toggle="pill" data-bs-target="#pills-${part.partNo}" type="button" role="tab" aria-controls="pills-${part.partNo}" aria-selected="true">Part ${part.partNo}</button>
                </li>
            `);
      tabcontent.append(`
                <div class="tab-pane fade show" id="pills-${part.partNo}" role="tabpanel"  aria-labelledby="pills-${part.partNo}-tab">
                    <div class="row justify-content-center px-5" style="height:65vh; box-sizing:border-box;">
                        <div class="col-md-12 mx-4 border border-dark px-0 rounded">
                            <div id="scrollspy-part${part.partNo}" class="scrollspy-example px-3 bg-secondary bg-opacity-10" style="overflow-y:scroll; height:65vh;" data-bs-spy="scroll" data-bs-target="#list-example" data-bs-offset="0" tabindex="0">
                                <div class="row d-flex justify-content-evenly mt-3">

                                </div>
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

    $(document).trigger("contentRendered");
  });

  // Cập nhật định dạng thời gian
  function formatTime(seconds) {
    const min = Math.floor(seconds / 60);
    const sec = Math.floor(seconds % 60)
      .toString()
      .padStart(2, "0");
    return `${min}:${sec}`;
  }

  // Khi âm thanh tải xong, hiển thị thời lượng
  function loadAudio(src) {
    $audio.attr("src", src); // Cập nhật src của audio
    $audio[0].load(); // Bắt buộc tải lại tệp âm thanh

    // Khi tệp âm thanh đã tải xong
    $audio.on("loadedmetadata", function () {
      $duration.text(formatTime($audio[0].duration));
      $progressBar.val(0);
      $currentTime.text("00:00");
    });
  }

  // Cập nhật tiến trình và thời gian phát hiện tại
  $audio.on("timeupdate", function () {
    const currentTime = $audio[0].currentTime;
    const duration = $audio[0].duration;
    const progress = (currentTime / duration) * 100;

    $progressBar.val(progress);
    $currentTime.text(formatTime(currentTime));
  });

  // Phát âm thanh
  //$("#btn-start").click(() => {
  //    $audio[0].play(); $('div[role="tabpanel"]').css({ 'opacity': '1' });

  //});

  // Tạm dừng âm thanh
  $("#pauseBtn").click(() => {
    $audio[0].pause();
  });

  // Dừng âm thanh và tua lại
  $("#stopBtn").click(() => {
    $audio[0].pause();
    $audio[0].currentTime = 0;
    $progressBar.val(0);
    $currentTime.text("00:00");
  });

  // Điều chỉnh âm lượng
  $("#volumeSlider").on("input", function () {
    $audio[0].volume = $(this).val();
  });

  // Tua đến vị trí trong tiến trình
  $progressBar.on("click", function (event) {
    const offsetX = event.offsetX;
    const totalWidth = $(this).width();
    const clickPosition = offsetX / totalWidth;
    $audio[0].currentTime = $audio[0].duration * clickPosition;
  });

  $(document).on("click", "#btnSubmit", function () {
    //const productId = $(this).data("id");
    if (confirm("Bạn có chắc muốn nộp bài?")) {
      var answers = [];
      answers.push(...getInputRadioElement());
      answers.push(...getInputCheckboxElement());
      answers.push(...getInputTextElement());
      answers.push(...getInputDropboxElement());
      console.log(answers);
      console.log(JSON.stringify(answers));
      var submitedAnswer = {
        questionBankId: QUESTION_BANK_ID,
        forQuestion: "listening",
        examCode: "sdbhjsd",
        answers: answers,
      };
      ApiService.gradeExamination(submitedAnswer, function (data) {
        console.log("result = " + data);
        alert("Your Grade is: " + data);
      });
    }
  });
  $("#btn-start").click(() => {
    $audio[0].play();
  });

  $(document).on("click", "div#downloadexcel", function () {
    var token = getCookie("token");
    $.ajax({
      url: `${decodeURIComponent(API_BASE_URL)}/QuestionBanks/template/download/listening`,
      type: "get",
      headers: {
        Authorization: "Bearer " + token,
      },
      // data: JSON.stringify(requestData),
      contentType: "application/json",
      xhrFields: {
        responseType: "blob", // Đặt kiểu phản hồi là blob
      },
      success: function (blob, status, xhr) {
        // Tạo URL từ Blob
        var downloadUrl = URL.createObjectURL(blob);
        // Tạo một thẻ a để tải về file
        var a = document.createElement("a");
        a.href = downloadUrl;
        a.download = "templateListening.xlsx"; // Tên file bạn muốn tải về
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);

        // Giải phóng bộ nhớ
        URL.revokeObjectURL(downloadUrl);
      },
      error: function (xhr, status, error) {
        console.log(xhr);
      },
    });
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

  function formatTimeCountdown(seconds) {
    const minutes = Math.floor(seconds / 60);
    const remainingSeconds = seconds % 60;
    return `${minutes}:${remainingSeconds.toString().padStart(2, "0")}`;
  }

  $(document).on("change", "input.file-picture", function () {
    const file = this.files[0]; // Lấy file từ DOM element
    if (!file) return; // Không làm gì nếu không có file

    const allowedExtensions = ["jpg", "jpeg", "png", "pdf"];
    const fileExtension = file.name.split(".").pop().toLowerCase();

    if (!allowedExtensions.includes(fileExtension)) {
      alert("Chỉ chấp nhận ảnh định dạng .jpg, .jpeg, .png, hoặc .pdf!");
      $(this).val(""); // Reset input
      return;
    }

    const reader = new FileReader();

    // Khi file được đọc xong
    reader.onload = (e) => {
      const parentDiv = $(this).closest("div"); // Tìm thẻ div chứa class file-picture
      const imgElement = parentDiv.find("div.picture"); // Tìm thẻ img trong div đó
      if (["jpg", "jpeg", "png"].includes(fileExtension))
        imgElement.html(`<img src="${e.target.result}" width="100%;" />`);
      else
        imgElement.html(
          `<embed class="" src="${e.target.result}" style="border: 0px;" width="100%;" height="100%;" />`
        );
      //imgElement.attr("src", e.target.result).attr("hidden", false); // Hiển thị ảnh
    };

    reader.readAsDataURL(file); // Đọc file dưới dạng Data URL
  });

  $(document).on("click", "button.save-picture", function () {
    const parentDiv = $(this).closest("div"); // Tìm thẻ div chứa class file-picture
    const inputEle = parentDiv.find("input.file-picture"); // Tìm thẻ img trong div đó
    var partNo = inputEle.data("partno");
    var groupNo = inputEle.data("groupno");

    const image = inputEle.prop("files")[0];

    const formData = new FormData();
    formData.append("questionBankId", QUESTION_BANK_ID);
    formData.append("partNo", partNo);
    formData.append("groupNo", groupNo);
    formData.append("forQuestion", "listening");
    formData.append("fileUploads", image);

    $.ajax({
      url: `${decodeURIComponent(API_BASE_URL)}/QuestionBanks/UploadPicture`,
      type: "POST",
      data: formData,
      contentType: false,
      processData: false,
      success: function (response) {
        alert("Successful!");
        location.reload();
      },
      error: function (error) {
        console.log("Error:", error);
      },
    });
  });

  $(document).on("click", "button#btn-file-excel", function () {
    var inputExcel = $(`#fileExcel`);
    var fileAudio = $(`#fileAudio`);
    var time = $(`#time`).val();

    const formData = new FormData();
    formData.append("questionBankId", QUESTION_BANK_ID);
    formData.append("forQuestion", "listening");
    formData.append("time", time);
    formData.append("fileUploads", inputExcel.prop("files")[0]);
    formData.append("fileAudio", fileAudio.prop("files")[0]);

    $.ajax({
      url: `${decodeURIComponent(API_BASE_URL)}/QuestionBanks/UploadQuestionExcel`,
      type: "POST",
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
  var scrollspy = $(`#scrollspy-part${part.partNo} .row`);
  var scrollspyitem = $(`#scrollspyitem-part${part.partNo}`);
  scrollspy.empty();
  scrollspyitem.empty();
  var groups = part.groups;
  groups.forEach(function (group) {
    var fileurl = "";
    if (group.fileType == "jpg" || group.fileType == "png" || group.fileType == "jpeg")
      fileurl = `<img src="${group.fileURL}" width="100%;" />`;
    else if (group.fileType == "pdf")
      fileurl = `<embed class="" src="${group.fileURL}#toolbar=0" style="border: 0px;" width="100%;" height="100%;" />`;

    scrollspy.append(`
             <div class="col-6">
                <input data-partno="${part.partNo}" data-groupno="${group.groupNo}" class="btn btn-muted file-picture" type="file" name="file" accept=".jpg, .jpeg, .png, .pdf" />
                <button class="btn btn-info save-picture">Save Picture</button>
                <div class="picture h-75">
                    ${fileurl}
                </div>
            </div>
            <div id="group-part${part.partNo}-group${group.groupNo}" data-type="${group.type}" class="col-6">
                <div class="mb-3 fw-bold">Questions ${group.questionRange}</div>
                <div class="mb-3">${group.title}</div>
                                
            </div>
            <hr class="my-5 text-lg border border-2 border-dark"/>
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
