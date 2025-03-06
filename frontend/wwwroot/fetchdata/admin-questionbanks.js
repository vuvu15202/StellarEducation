const API_BASE_URL = getCookie("apiurl") || "http://localhost:5000/api";

//let options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };

// Init When Load pageconst initPage = () => {
const initPage = async () => {
    //document.getElementById('projectsActive').className = 'active';
    //document.getElementById("ProjectIdMomo").value = projectId;
    //document.getElementById("ProjectIdVnPay").value = projectId;
    //console.log(document.getElementById("ProjectIdVnPay").value)
    await pushDataOnLoad();
}
document.addEventListener("DOMContentLoaded", initPage);

// ---------------------------------- Call API ----------------------------------
async function getAllQuestionBanks() {
    const callApi = async (API_BASE_URL) => {
        return (await fetch(API_BASE_URL)).json();
    }
    return await callApi(`${decodeURIComponent(API_BASE_URL)}/QuestionBanks`);
}

// -------------------------------------------------------------------------------
let questionBanksGlobal = [];
let categoriesGlobal = [];

async function pushDataOnLoad() {


    getAllQuestionBanks().then(async (questionBanks) => {
        //const [amountData] = await Promise.all([getTotalAmountProject(projectId)]);
        questionBanksGlobal = questionBanks;
        $.each(questionBanks, function (index, value) {
            $("#questionBanks").append(`
                <tr>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold w-25 text-wrap">${index + 1}</span>
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold w-25 text-wrap">${value.examCode}</span>
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold">${value.startDateString}</span>        
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold">${value.endDateString}</span>
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold">${value.isClosed}</span>
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold">${value.lecturer.lastName} ${value.lecturer.firstName}</span>
                    </td>
                    <td class="align-middle">
                        <div>
                            <button class="btn btn-light mb-0" type="button" id="ellipsisMenu" data-bs-toggle="dropdown" aria-expanded="false">
                                <i class="fas fa-ellipsis-v"></i>
                            </button>
                            <!-- Dropdown Menu -->
                            <ul class="dropdown-menu" aria-labelledby="ellipsisMenu">
                                <li>
                                    <a href="/admin/questionbankEdit/index?questionbankid=${value.questionBankId}" target="_blank">
                                    <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4">
                                        <i class="fas fa-file-pdf text-lg me-1"></i>
                                        Quản Lí Đề Thi
                                    </button>
                                    </a>
                                    </hr>
                                </li>
                                <li>
                                    <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4 viewCandidates" data-id="${value.questionBankId}" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                                        <i class="fas fa-users text-lg me-1" style="color: #5c98ff;"></i>                                        
                                        DS thí sinh
                                    </button>
                                    </hr>
                                </li>
                                <li>
                                    <button class="btn btn-link text-danger text-sm mb-0 px-0 ms-4" data-id="${value.questionBankId}">
                                        <a id="exportLink" href="${decodeURIComponent(API_BASE_URL)}/ExamCandidates/exportcadidates/${value.questionBankId}" download="reportResultQuestionBank_5.xlsx">
                                            <i class="fas fa-download text-lg me-1" style="color: #bc00f0;"></i> Xuất bảng điểm
                                        </a>
                                    </button>
                                </li>
                                <li>
                                    <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4 editQuestionBank" data-id="${value.questionBankId}">
                                        <i class="fas fa-edit" style="color: #38d100; font-size: 17px;"></i>
                                        Sửa
                                    </button>
                                </li>
                                <li>
                                    <button class="btn btn-link text-danger text-sm mb-0 px-0 ms-4 deleteRecord" data-id="${value.questionBankId}">
                                        <i class="fas fa-trash-alt fa-lg" style="color: #ff0000; font-size: 17px;""></i>                                
                                        Xóa
                                    </button>
                                </li>
                            </ul>
                        </div>
                    </td>
                </tr>
            `);
        });


    })


}


$('#searchQuestionBank').on('input', function () {
    var searchText = $(this).val().toLowerCase();
    var filteredQuestionBanks = questionBanksGlobal.filter(function (questionBank) {
        return questionBank.examCode.toLowerCase().indexOf(searchText) !== -1;
    });
    // showStudentList(filteredQuestionBanks);
    $("#questionBanks").html("");
    $.each(filteredQuestionBanks, function (index, value) {
        $("#questionBanks").append(`
                <tr>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold w-25 text-wrap">${index + 1}</span>
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold w-25 text-wrap">${value.examCode}</span>
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold">${value.startDate}</span>        
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold">${value.endDate}</span>
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold">${value.isClosed}</span>
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold">${value.lecturer.lastName} ${value.lecturer.firstName}</span>
                    </td>
                    <td class="align-middle">
                        <div>
                            <button class="btn btn-light mb-0" type="button" id="ellipsisMenu" data-bs-toggle="dropdown" aria-expanded="false">
                                <i class="fas fa-ellipsis-v"></i>
                            </button>
                            <!-- Dropdown Menu -->
                            <ul class="dropdown-menu" aria-labelledby="ellipsisMenu">
                                <li>
                                    <a href="/admin/questionbankEdit/index?questionbankid=${value.questionBankId}" target="_blank">
                                    <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4">
                                        <i class="fas fa-file-pdf text-lg me-1"></i>
                                        Quản Lí Đề Thi
                                    </button>
                                    </a>
                                    </hr>
                                </li>
                                <li>
                                    <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4 viewCandidates" data-id="${value.questionBankId}" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                                        <i class="fas fa-users text-lg me-1" style="color: #5c98ff;"></i>                                        
                                        Danh sách thí sinh
                                    </button>
                                    </hr>
                                </li>
                                <li>
                                    <button class="btn btn-link text-danger text-sm mb-0 px-0 ms-4" data-id="${value.questionBankId}">
                                        <a id="exportLink" href="http://localhost:5000/api/ExamCandidates/exportcadidates/${value.questionBankId}" download="reportResultQuestionBank_5.xlsx">
                                            <i class="fas fa-download text-lg me-1" style="color: #bc00f0;"></i> Xuất bảng điểm
                                        </a>
                                    </button>
                                </li>
                                <li>
                                    <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4 editQuestionBank" data-id="${value.questionBankId}">
                                        <i class="fas fa-edit" style="color: #38d100; font-size: 17px;"></i>
                                        Sửa
                                    </button>
                                </li>
                                <li>
                                    <button class="btn btn-link text-danger text-sm mb-0 px-0 ms-4 deleteRecord" data-id="${value.questionBankId}">
                                        <i class="fas fa-trash-alt fa-lg" style="color: #ff0000; font-size: 17px;""></i>                                
                                        Xóa
                                    </button>
                                </li>
                            </ul>
                        </div>
                    </td>
                </tr>
            `);
    });
});
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
$(document).ready(function () {
    var token = getCookie('token');
    $("#createQuestionBank").click(function () {
        var id = $('#questionBankId').val();

        var json = {
            questionBankId: parseInt(id),
             examCode : $('#examCode').val(),
             startDate : $('#startDate').val(),
            endDate: $('#endDate').val(),
            //time: parseInt($('#time').val()),
            isClosed: $('#isClosed').prop('checked'),
            isPrivate: $('#isPrivate').prop('checked'),
        };

        //const formData = new FormData();
        //formData.append('courseId', courseId);
        //formData.append('categoryId', categoryId);
        //formData.append('image', image);
        //formData.append('name', name);
        //formData.append('description', description);
        //formData.append('isPrivate', isPrivate);
        //formData.append('isDelete', isDelete);
        //formData.append('price', price);


        if (id == 0) {
            $.ajax({
                url: `${decodeURIComponent(API_BASE_URL)}/QuestionBanks`,
                type: 'POST',
                //data: formData,
                //contentType: false, for formdata
                //processData: false, for formdata
                data: JSON.stringify(json),
                contentType: "application/json",
                headers: {
                    Authorization: `Bearer ${token}` 
                },
                success: function (response) {
                    console.log('Server response:', response);
                    location.reload();
                },
                error: function (error) {
                    console.log('Error:', error);
                    alert(error.responseText);
                }
            });
        } else {
            $.ajax({
                url: `${decodeURIComponent(API_BASE_URL)}/QuestionBanks/${id}`,
                type: 'PUT',
                //data: formData,
                //contentType: false, for formdata
                //processData: false, for formdata
                data: JSON.stringify(json),
                contentType: "application/json",
                headers: {
                    Authorization: `Bearer ${token}`
                },
                success: function (response) {
                    console.log('Server response:', response);
                    location.reload();
                },
                error: function (error) {
                    console.log('Error:', error);
                    alert(error.responseText);
                }
            });
        }
    });



    $(document).on('click', '.deleteRecord', function () {
        let id = $(this).attr('course-id');
        if (confirm("Bạn có chắc muốn xóa khóa học này không!")) {
            $.ajax({
                type: "delete",
                url: `${decodeURIComponent(API_BASE_URL)}/Courses/${id}`,
                success: function (result, status, xhr) {
                    if (confirm('Xóa khóa học thành công!')) {
                        location.reload();
                    }
                },
                error: function (xhr, status, error) {
                    console.log(xhr)
                }
            });
        }
    });

    $(document).on('click', '.viewCandidates', function () {
        let id = $(this).data('id');
        $.ajax({
            type: "get",
            url: `${decodeURIComponent(API_BASE_URL)}/ExamCandidates/candidates/${id}`,
            success: function (value, status, xhr) {
                $("#file-candidates").attr("data-questionbankid", id);
                $("#candidates").html(``);
                $.each(value, function (index, examCandidate) {
                    var isComplete = examCandidate.isComplete ? `<i class="fas fa-check" style="color: #63E6BE;"></i>` : ``;
                    var gradebtn = examCandidate.bandScoreWriting == 0
                        && examCandidate.questionBank.writingJSON.parts
                        && examCandidate.isComplete ? `
                                        <button type="button" class="btn p-0 my-0 mx-3"
                                            data-bs-toggle="tooltip" data-bs-placement="top"
                                            data-bs-custom-class="custom-tooltip"
                                            data-bs-title="Grade writing for this student.">
                                        <a target="_blank" href="/admin/questionbankEdit/writing?questionbankid=${examCandidate.questionBankId}&examcandidateid=${examCandidate.examCandidateId}">
                                            <i class="fas fa-edit" style="color: #FFD43B;"></i> Grade
                                        </a>
                                    </button>` : ``;
                    $("#candidates").append(`
                        <tr>
                            <td class="align-middle">
                                <span class="text-xs font-weight-bold w-25 text-wrap">${index + 1}</span>
                            </td>
                            <td class="align-middle">
                                <span class="text-xs font-weight-bold w-25 text-wrap">${examCandidate.candidate.firstName}</span>
                            </td>
                            <td class="align-middle">
                                <span class="text-xs font-weight-bold">${examCandidate.candidate.lastName}</span>        
                            </td>
                            <td class="align-middle">
                                <span class="text-xs font-weight-bold">
                                    <span class="text-xs font-weight-bold">${examCandidate.candidate.email}</span>
                                </span>
                            </td>
                            <td class="align-middle">
                                <span class="text-xs font-weight-bold">${examCandidate.candidate.phone}</span>
                            </td>
                            <td class="align-middle">
                                ${isComplete}
                                ${gradebtn}
                            </td>
                        </tr>
                    `);
                });
            },
            error: function (xhr, status, error) {
                console.log(xhr)
            }
        });
    });

    $(document).on('click', '#submit-file-candidates', function () {
        let id = $("#file-candidates").data('questionbankid');

        const formData = new FormData();
        formData.append('questionBankId', id);
        formData.append('fileUploads', $("#file-candidates").prop('files')[0]);

        $.ajax({
            url: `${decodeURIComponent(API_BASE_URL)}/ExamCandidates/UploadCandidatesExcel`,
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

    $('#clearQuestionBank').click(function () {
        $('#questionBankId').val('0');
        $('#examCode').val('');
        $('#startDate').val('');
        $('#endDate').val('');
        $('#time').val(60);
        $('#isClosed').prop('checked', false);
        $('#isPrivate').prop('checked', true);
    });


    $(document).on('click', '.editQuestionBank', function () {
        let id = $(this).data('id'); 
        var questionBank = questionBanksGlobal.find(c => c.questionBankId == id);

        $('#questionBankId').val(questionBank.questionBankId);
        $('#examCode').val(questionBank.examCode);
        $('#startDate').val(questionBank.startDate.slice(0, 16));
        $('#endDate').val(questionBank.endDate.slice(0, 16));
        $('#time').val( questionBank.time);
        $('#isClosed').prop('checked', questionBank.isClosed);
        $('#isPrivate').prop('checked', questionBank.isPrivate);
    });


    
});
