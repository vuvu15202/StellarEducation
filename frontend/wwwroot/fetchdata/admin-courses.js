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
async function getAllCourses() {
    const callApi = async (API_BASE_URL) => {
        return (await fetch(API_BASE_URL)).json();
    }
    return await callApi(`${decodeURIComponent(API_BASE_URL)}/Courses`);
}

async function getAllCatogory() {
    const callApi = async (API_BASE_URL) => {
        return (await fetch(API_BASE_URL)).json();
    }
    return await callApi(`${decodeURIComponent(API_BASE_URL)}/Categories`);
}

async function getAllUsers() {
    const callApi = async (API_BASE_URL) => {
        return (await fetch(API_BASE_URL)).json();
    }
    return await callApi(`${decodeURIComponent(API_BASE_URL)}/Courses/GetAllLecture`);
}


// -------------------------------------------------------------------------------
let coursesGlobal = [];
let categoriesGlobal = [];
let userGlobal = [];
async function pushDataOnLoad() {


    getAllCourses().then(async (courses) => {
        //const [amountData] = await Promise.all([getTotalAmountProject(projectId)]);
        coursesGlobal = courses;
        $.each(courses, function (index, value) {
            $("#courses").append(`
                <tr>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold w-25 text-wrap">${index + 1}</span>
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold w-25 text-wrap">${value.name}</span>
                    </td>
                    <td class="align-middle">
                        <image src="${value.image}" style="height: 50px; width: fit-content;">               
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold">${value.price} VND</span>
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold">${value.isDelete}</span>
                    </td>
                    <td class="align-middle">
                        <div>
                            <button class="btn btn-light mb-0" type="button" id="ellipsisMenu" data-bs-toggle="dropdown" aria-expanded="false">
                                <i class="fas fa-ellipsis-v"></i>
                            </button>
                            <!-- Dropdown Menu -->
                            <ul class="dropdown-menu" aria-labelledby="ellipsisMenu">
                                <li>
                                    <a href="javascript:void(0)" data-id="${value.courseId}" class="viewprojectbills">
                                    <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                                        <i class="fas fa-file-pdf text-lg me-1"></i>
                                        Xem
                                    </button>
                                    </a>
                                    </hr>
                                </li>
                                <li>
                                    <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4 viewStudentInCourse" data-courseid="${value.courseId}" data-bs-toggle="modal" data-bs-target="#staticBackdropStudents">
                                        <i class="fas fa-users text-lg me-1" style="color: #5c98ff;"></i>                                        
                                        DS Học Sinh
                                    </button>
                                    </hr>
                                </li>
                                <li>
                                    <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4 viewCourse" course-id="${value.courseId}">
                                        <i class="fas fa-edit" style="color: #38d100; font-size: 17px;"></i>
                                        Sửa
                                    </button>
                                </li>
                                <li>
                                    <button class="btn btn-link text-danger text-sm mb-0 px-0 ms-4 deleteRecord" course-id="${value.courseId}">
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


    getAllCatogory().then(categories => {
        categoriesGlobal = categories;
        const categorySelect = document.getElementById('categoryId');
        categories.forEach(category => {
            const option = document.createElement('option');
            option.value = category.categoryId;
            option.textContent = category.name;
            categorySelect.appendChild(option);
        });
    });

    getAllUsers().then(users => {
        userGlobal = users;
        const userSelect = document.getElementById('lecturerId');
        users.forEach(user => {
            const option = document.createElement('option');
            option.value = user.lecturerId; 
            option.textContent = user.userName; 
            userSelect.appendChild(option);
        });
    });
}


$('#searchCourse').on('input', function () {
    var searchText = $(this).val().toLowerCase();
    var filteredStudents = coursesGlobal.filter(function (course) {
        return course.name.toLowerCase().indexOf(searchText) !== -1 ;
    });
    // showStudentList(filteredStudents);
    $("#courses").html("");
    $.each(filteredStudents, function (index, value) {
        $("#courses").append(`
            <tr>
                <td class="align-middle">
                    <span class="text-xs font-weight-bold w-25 text-wrap">${index + 1}</span>
                </td>
                <td class="align-middle">
                    <span class="text-xs font-weight-bold w-25 text-wrap">${value.name}</span>
                </td>
                <td class="align-middle">
                    <image src="${value.image}" style="height: 50px; width: fit-content;">               
                </td>
                <td class="align-middle">
                    <span class="text-xs font-weight-bold">${value.price} VND</span>
                </td>
                <td class="align-middle">
                    <span class="text-xs font-weight-bold">${value.isDelete}</span>
                </td>
                <td class="align-middle">
                    <div>
                        <button class="btn btn-light mb-0" type="button" id="ellipsisMenu" data-bs-toggle="dropdown" aria-expanded="false">
                            <i class="fas fa-ellipsis-v"></i>
                        </button>
                        <ul class="dropdown-menu" aria-labelledby="ellipsisMenu">
                            <li>
                                <a href="javascript:void(0)" data-id="${value.courseId}" class="viewprojectbills">
                                <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                                    <i class="fas fa-file-pdf text-lg me-1"></i>
                                    Xem
                                </button>
                                </a>
                                </hr>
                            </li>
                            <li>
                                <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4 viewStudentInCourse" data-courseid="${value.courseId}" data-bs-toggle="modal" data-bs-target="#staticBackdropStudents">
                                    <i class="fas fa-users text-lg me-1" style="color: #5c98ff;"></i>                                        
                                    DS Học Sinh
                                </button>
                                </hr>
                            </li>
                            <li>
                                <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4 viewCourse" course-id="${value.courseId}">
                                    <i class="fas fa-edit" style="color: #38d100; font-size: 17px;"></i>
                                    Sửa
                                </button>
                            </li>
                            <li>
                                <button class="btn btn-link text-danger text-sm mb-0 px-0 ms-4 deleteRecord" course-id="${value.courseId}">
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
    $("#createCourse").click(function () {
        const courseId = $('#courseId').val();
        const categoryId = $('#categoryId').val();
        const name = $('#name').val();
        const image = $('#image').prop('files')[0];
        const description = $('#description').val();
        const isPrivate = $('#isPrivate').prop('checked');
        const isDelete = $('#isDelete').prop('checked');
        const price = $('#price').val();
        const lecturerId = $('#lecturerId').val();

        if (!categoryId) { alert("Vui lòng chọn danh mục!"); return; }
        if (!lecturerId) { alert("Vui lòng chọn giản viên!"); return; }

        const formData = new FormData();
        formData.append('courseId', courseId);
        formData.append('categoryId', categoryId);
        formData.append('image', image);
        formData.append('name', name);
        formData.append('description', description);
        formData.append('isPrivate', isPrivate);
        formData.append('isDelete', isDelete);
        formData.append('price', price);
        formData.append('lecturerId', lecturerId);

        if (courseId == 0) {
            $.ajax({
                url: `${decodeURIComponent(API_BASE_URL)}/Courses`,
                type: 'POST',
                headers: {
                    Authorization: `Bearer ${token}` 
                },
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    alert("Create Course Successfully!");
                    location.reload();
                },
                error: function (xhr, status, error) {
                    alert(xhr.responseText);
                }
            });
        } else {
            $.ajax({
                url: `${decodeURIComponent(API_BASE_URL)}/Courses/${courseId}`,
                type: 'PUT',
                headers: {
                    Authorization: `Bearer ${token}` 
                },
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    alert("Update Course Successfully!");
                    location.reload();
                },
                error: function (xhr, status, error) {
                    alert(xhr.responseText);
                }
            });
        }

        //$.ajax({
        //    type: "post",
        //    url: "http://localhost:5000/api/Courses",
        //    data: JSON.stringify(formData),
        //    contentType: "application/json",
        //    success: function (result, status, xhr) {
        //        if (confirm('Thêm khóa học thành công!')) {
        //            location.reload();
        //        }
        //    },
        //    error: function (xhr, status, error) {
        //        console.log(xhr)
        //    }
        //});
    });



    $(document).on('click', '.deleteRecord', function () {
        let id = $(this).attr('course-id');
        if (confirm("Bạn có chắc muốn xóa khóa học này không!")) {
            $.ajax({
                type: "delete",
                url: `${decodeURIComponent(API_BASE_URL)}/Courses/${id}`,
                headers: {
                    Authorization: `Bearer ${token}`
                },
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



    $('#clearCourse').click(function () {

        $('#courseId').val('0');
        $('#name').val('');
        $('#description').val('');
        $('#isPrivate').prop('checked', true);
        $('#isDelete').prop('checked', false);
        $('#image').val('');
        $('#price').val('0');
        $('#categoryId option:first').prop('selected', true);
        $('#lecturerId option:first').prop('selected', true);
    });


    $(document).on('click', '.viewCourse', function () {
        let courseId = $(this).attr('course-id');
        var course = coursesGlobal.find(c => c.courseId == courseId);

        $('#courseId').val(course.courseId);
        $('#name').val(course.name);
        $('#description').val(course.description);
        $('#isPrivate').prop('checked', course.isPrivate);
        $('#isDelete').prop('checked', course.isDelete);
        $('#price').val(course.price);
        $('#categoryId').val(course.categoryId);
        $('#lecturerId').val(course.lecturerId);
    });

    $(document).on('click', '.viewStudentInCourse', function () {
        let courseId = $(this).data('courseid');
        $("#file-candidates").attr("data-courseid", courseId);

        $.ajax({
            url: `${decodeURIComponent(API_BASE_URL)}/courseenrolls/getcourseenrollsbycourseid/${courseId}`,
            type: 'GET',
            headers: {
                Authorization: `Bearer ${token}`
            },
            success: function (response) {
                console.log(response)
                $.each(response, function (index, value) {
                    $("#students").append(`
                        <tr>
                            <td class="align-middle">
                                <span class="text-xs font-weight-bold w-25 text-wrap">${index + 1}</span>
                            </td>
                            <td class="align-middle">
                                <span class="text-xs font-weight-bold w-25 text-wrap">${value.user.firstName} ${value.user.lastName}</span>
                            </td>
                            <td class="align-middle">
                                <span class="text-xs font-weight-bold">
                                    <span class="text-xs font-weight-bold">${value.user.email}</span>
                                </span>
                            </td>
                            <td class="align-middle">
                                <span class="text-xs font-weight-bold">${value.enrollDate}</span>        
                            </td>
                            <td class="align-middle">
                                <span class="text-xs font-weight-bold">${value.lessonCurrent}</span>        
                            </td>
                            <td class="align-middle">
                                <span class="text-xs font-weight-bold">${value.courseStatus}</span>        
                            </td>
                            <td class="align-middle">
                                <span class="text-xs font-weight-bold">${value.averageGrade == null ? 'N/A' : value.averageGrade}</span>        
                            </td>
                        </tr>
                    `);
                });
            },
            error: function (error) {
                console.log('Error:', error);
            }
        });
    });

    $(document).on('click', '#submit-file-candidates', function () {
        let id = $("#file-candidates").data('courseid');

        const formData = new FormData();
        formData.append('courseId', id);
        formData.append('fileUploads', $("#file-candidates").prop('files')[0]);

        $.ajax({
            url: `${decodeURIComponent(API_BASE_URL)}/Courses/UploadStudentsExcel`,
            type: 'POST',
            headers: {
                Authorization: `Bearer ${token}`
            },
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                alert("Successful!");
                location.reload();
            },
            error: function (xhr, status, error) {
                alert(error);
                console.log('Error:', xhr.responseText);
            }
        });
    });

    $(document).on('click', '.viewprojectbills', function () {
        let id = $(this).attr('data-id'); console.log(id);

        var course = coursesGlobal.find(course => course.courseId == id); console.log(coursesGlobal);
        $("#course-detail").html('');
        $("#course-detail").append(`
                            <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    <img width="300px;" src='${course.image}'>
                                </div>
                             </li>

                              <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    ID:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${course.courseId}
                                </div>
                             </li>
                             <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    Tên Khóa học:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${course.name}
                                </div>
                             </li>
                             <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    Giá bán:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${course.price}
                                </div>
                             </li>
                             <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    Chế độ riêng tư:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${course.isPrivate ? "Yes": "No"}
                                </div>
                             </li>
                             <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    Mô tả:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${course.description}
                                </div>
                             </li>
                             <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    Danh mục:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${course.category.name}
                                </div>
                             </li>
        `);

        
    });
});
