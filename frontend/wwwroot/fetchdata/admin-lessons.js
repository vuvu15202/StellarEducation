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

// -------------------------------------------------------------------------------
let coursesGlobal = [];
let categoriesGlobal = [];

async function pushDataOnLoad() {


    getAllCourses().then(async (courses) => {
        //const [amountData] = await Promise.all([getTotalAmountProject(projectId)]);
        coursesGlobal = courses;

        const courseSelect = document.getElementById('courseId');
        const courseSelectCreate = document.getElementById('courseIdCreate');

        courses.forEach(category => {
            const option = document.createElement('option');
            option.value = category.courseId;
            option.textContent = category.name;
            courseSelectCreate.appendChild(option);
        });
        courses.forEach(category => {
            const option = document.createElement('option');
            option.value = category.courseId;
            option.textContent = category.name;
            courseSelect.appendChild(option);
        });


        $('#courseId option:eq(1)').prop('selected', true);

        //display first lesson course
        const courseId = courses[0].courseId;

        var filteredStudents = courses.find(c => c.courseId == courseId);
        // showStudentList(filteredStudents);
        $("#lessons").html("");
        $.each(filteredStudents.lessons, function (index, value) {
            $("#lessons").append(`
                    <tr>
                        <td class="align-middle">
                            <span class="text-xs font-weight-bold w-25 text-wrap">${value.lessonNum}</span>
                        </td>
                        <td class="align-middle">
                            <span class="text-xs font-weight-bold w-25 text-wrap">${value.previousLessioNum}</span>
                        </td>
                        <td class="align-middle">
                            <span class="text-xs font-weight-bold">${value.name}</span>
                        </td>
                        <td class="align-middle">
                            <span class="text-xs font-weight-bold"> <a class="text-info" href="https://www.youtube.com/watch?v=${value.videoUrl}" target="_blank">${value.videoUrl}</a></span>
                        </td>
                        <td class="align-middle">
                            <span class="text-xs font-weight-bold">${value.quiz ?? ""}</span>
                        </td>
                        <td class="align-middle">
                            <div>
                                <button class="btn btn-light mb-0" type="button" id="ellipsisMenu" data-bs-toggle="dropdown" aria-expanded="false">
                                    <i class="fas fa-ellipsis-v"></i>
                                </button>
                                <ul class="dropdown-menu" aria-labelledby="ellipsisMenu">
                                    <li>
                                        <a href="javascript:void(0)" course-id="${courseId}" lesson-num="${value.lessonNum}" class="viewprojectbills">
                                            <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                                                <i class="fas fa-file-pdf text-lg me-1"></i>
                                                Xem
                                            </button>
                                        </a>
                                    </li>
                                    <li>
                                        <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4 viewLesson" course-id="${courseId}" lesson-num="${value.lessonNum}">
                                            <i class="fas fa-edit" style="color: #38d100; font-size: 17px;"></i>
                                            Sửa
                                        </button>
                                        </hr>
                                    </li>
                                    <li>
                                        <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4 deleteRecord" lesson-id="${value.lessonId}">
                                            <i class="fas fa-trash-alt fa-lg" style="color: #ff0000; font-size: 17px;""></i>                                
                                            Xóa
                                        </button>
                                        </hr>
                                    </li>
                                </ul>
                            </div>
                        </td>
                    </tr>
            `);
        });
        //$.each(courses, function (index, value) {
        //    $("#courses").append(`<li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
        //            <div class="d-flex flex-column">
        //                <h6 class="mb-1 text-dark font-weight-bold text-sm">${value.name}</h6>
        //                <span class="text-xs">${value.courseId}</span>
        //            </div>
        //             <div class="d-flex align-items-center text-sm">
        //                ${value.price} VND
        //            </div>
        //            <div class="d-flex align-items-center text-sm">
        //                ${value.name}
        //            </div>
        //            <a href="javascript:void(0)" data-id="${value.projectId}" class="viewprojectbills">
        //                <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
        //                    <i class="fas fa-file-pdf text-lg me-1"></i>
        //                    Xem
        //                </button>
        //            </a>
        //            </li>
        //    `);
        //});


    })


    
}


$('#courseId').on('change', function () {
    const courseId = $(this).val();
    const courseName = $(this).find("option:selected").text();

    var filteredStudents = coursesGlobal.find(c => c.courseId == courseId);
    // showStudentList(filteredStudents);
    $("#lessons").html("");
    $.each(filteredStudents.lessons, function (index, value) {
        $("#lessons").append(`
            <tr>
                <td class="align-middle">
                    <span class="text-xs font-weight-bold w-25 text-wrap">${value.lessonNum}</span>
                </td>
                <td class="align-middle">
                    <span class="text-xs font-weight-bold w-25 text-wrap">${value.previousLessioNum}</span>
                </td>
                <td class="align-middle">
                    <span class="text-xs font-weight-bold">${value.name}</span>
                </td>
                <td class="align-middle">
                    <span class="text-xs font-weight-bold"> <a class="text-info" href="https://www.youtube.com/watch?v=${value.videoUrl}" target="_blank">${value.videoUrl}</a></span>
                </td>
                <td class="align-middle">
                    <span class="text-xs font-weight-bold">${value.quiz ?? ""}</span>
                </td>
                <td class="align-middle">
                    <div>
                        <button class="btn btn-light mb-0" type="button" id="ellipsisMenu" data-bs-toggle="dropdown" aria-expanded="false">
                            <i class="fas fa-ellipsis-v"></i>
                        </button>
                        <ul class="dropdown-menu" aria-labelledby="ellipsisMenu">
                            <li>
                                <a href="javascript:void(0)" course-id="${courseId}" lesson-num="${value.lessonNum}" class="viewprojectbills">
                                    <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                                        <i class="fas fa-file-pdf text-lg me-1"></i>
                                        Xem
                                    </button>
                                </a>
                            </li>
                            <li>
                                <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4 viewLesson" course-id="${courseId}" lesson-num="${value.lessonNum}">
                                    <i class="fas fa-edit" style="color: #38d100; font-size: 17px;"></i>
                                    Sửa
                                </button>
                                </hr>
                            </li>
                            <li>
                                <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4 deleteRecord" lesson-id="${value.lessonId}">
                                    <i class="fas fa-trash-alt fa-lg" style="color: #ff0000; font-size: 17px;""></i>                                
                                    Xóa
                                </button>
                                </hr>
                            </li>
                        </ul>
                    </div>
                </td>
            </tr>
        `);
    });
});


//$('#searchCourse').on('input', function () {
//    var searchText = $(this).val().toLowerCase();
//    var filteredStudents = coursesGlobal.filter(function (course) {
//        return course.name.toLowerCase().indexOf(searchText) !== -1 ;
//    });
//    // showStudentList(filteredStudents);
//    $("#courses").html("");
//    $.each(filteredStudents, function (index, value) {
//        $("#courses").append(`<li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
//                    <div class="d-flex flex-column">
//                        <h6 class="mb-1 text-dark font-weight-bold text-sm">${value.name}</h6>
//                        <span class="text-xs">${value.courseId}</span>
//                    </div>
//                     <div class="d-flex align-items-center text-sm">
//                        ${value.price} VND
//                    </div>
//                    <div class="d-flex align-items-center text-sm">
//                        ${value.name}
//                    </div>
//                    <a href="javascript:void(0)" course-id="${value.projectId}" class="viewprojectbills">
//                        <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
//                            <i class="fas fa-file-pdf text-lg me-1"></i>
//                            Xem
//                        </button>
//                    </a>
//                    </li>
//            `);
//    });
//});

$(document).ready(function () {
    
    $('#createLesson').click(function () {
        // Hiển thị hộp thoại xác nhận
            // Lấy giá trị của tất cả các input
        const lessonId = $('#lessonId').val();
        const lessonNum = $('#lessonNum').val();
        const courseId = $('#courseIdCreate').val();
        const name = $('#name').val();
        const description = $('#description').val();
        const videoUrl = $('#videoUrl').val();
        const videoTime = $('#videoTime').val();
        const quiz = $('#quiz').val();
        const previousLessioNum = $('#previousLessioNum').val();
        const isDelete = $('#isDelete').prop('checked');

        if (!courseId) { alert("Vui lòng chọn một khóa học!"); return; }
        if (lessonNum < previousLessioNum) { alert("Lesson Number phải lớn hơn Previous Lesson Number."); return; }

        // Tạo một đối tượng FormData để chứa các giá trị
        const formData = new FormData();
        formData.append('lessonId', lessonId);
        formData.append('lessonNum', lessonNum);
        formData.append('courseId', courseId);
        formData.append('name', name);
        formData.append('description', description);
        formData.append('videoUrl', videoUrl);
        formData.append('videoTime', videoTime);
        formData.append('quiz', quiz);
        formData.append('previousLessioNum', previousLessioNum);
        formData.append('isDelete', isDelete);

        if (lessonId == 0) {
            $.ajax({
                url: `${decodeURIComponent(API_BASE_URL)}/Lessons`,
                type: 'POST',
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    console.log('Server response:', response);
                    // Làm mới trang sau khi gửi thành công
                    location.reload();
                },
                error: function (xhr, status, error) {
                    console.log('Error:', error);
                    alert(xhr.responseText);
                }
            });
        } else {
            $.ajax({
                url: `${decodeURIComponent(API_BASE_URL)}/Lessons/${lessonId}`,
                type: 'PUT',
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    console.log('Server response:', response);
                    // Làm mới trang sau khi gửi thành công
                    location.reload();
                },
                error: function (xhr, status, error) {
                    console.log('Error:', error); 
                    alert(xhr.responseText);
                }
            });
        }
    });


    $(document).on('click', '.deleteRecord', function () {
        let id = $(this).attr('lesson-id');
        if (confirm("Bạn có chắc muốn xóa khóa học này không!")) {
            $.ajax({
                type: "delete",
                url: `${decodeURIComponent(API_BASE_URL)}/Lessons/${id}`,
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

    $('#clearLesson').click(function () {
        $('#lessonId').val('0');
        $('#lessonNum').val('1');
        $('#courseIdCreate option:first').prop('selected', true);
        $('#name').val('');
        $('#description').val('');
        $('#videoUrl').val('');
        $('#videoTime').val('120');
        $('#previousLessioNum').val('0');
        $('#courseId').val('');
        $('#isDelete').prop('checked', false);
    });


    $(document).on('click', '.viewLesson', function () {
        let courseId = $(this).attr('course-id');
        let lessonNum = $(this).attr('lesson-num');
        console.log(lesson);


        var course = coursesGlobal.find(c => c.courseId == courseId);
        var lesson = course.lessons.find(l => l.lessonNum == lessonNum);

        $('#lessonId').val(lesson.lessonId);
        $('#lessonNum').val(lesson.lessonNum);
        $('#courseIdCreate').val(lesson.courseId);
        $('#name').val(lesson.name);
        $('#description').val(lesson.description);
        $('#videoUrl').val(lesson.videoUrl);
        $('#videoTime').val(lesson.videoTime);
        $('#previousLessioNum').val(lesson.previousLessioNum);
        $('#courseId').val(lesson.courseId);
        $('#quiz').val(lesson.quiz);
        $('#isDelete').prop('checked', course.isDelete);
    });


    $(document).on('click', '.viewprojectbills', function () {
        let courseId = $(this).attr('course-id');
        let lessonNum = $(this).attr('lesson-num');


        var course = coursesGlobal.find(c => c.courseId == courseId); 
        var lesson = course.lessons.find(l => l.lessonNum == lessonNum);
        $("#course-detail").html('')
        $("#course-detail").append(`
                            <li class="list-group-item border-0 ">
                                <div>
                                    <div class="w-100" id="player"></div>
                                </div>
                             </li>

                              <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    LessonNum:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${lesson.lessonNum}
                                </div>
                             </li>
                             <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    Tên Bài Giảng:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${lesson.name}
                                </div>
                             </li>
                             <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    Mô tả:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${lesson.description}
                                </div>
                             </li>
                             <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    Video Url:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${lesson.videoUrl}
                                </div>
                             </li>
                             
        `);
        

       
        onYouTubeIframeAPIReady(lesson.videoUrl);
        
    });




    //youtube
    var player;

    // This function creates an <iframe> (and YouTube player)
    // after the API code downloads.
    function onYouTubeIframeAPIReady(videoId) {
        player = new YT.Player('player', {
            height: '600',
            width: '1200',
            videoId: videoId, // Thay thế VIDEO_ID bằng ID của video YouTube
            events: {
                'onReady': onPlayerReady,
                'onStateChange': onPlayerStateChange
            }
        });
    }

    // The API will call this function when the video player is ready.
    function onPlayerReady(event) {
        // Bắt đầu phát video (tùy chọn)
        // event.target.playVideo();
    }

    function onPlayerStateChange(event) {
        if (event.data == YT.PlayerState.PLAYING) {
            setInterval(function () {
                var currentTime = player.getCurrentTime();
                if (currentTime > 10 && lessonNum == courseenrollGlobal.lessonCurrent) {
                    delete courseenrollGlobal['course'];
                    delete courseenrollGlobal['user'];
                    courseenrollGlobal.lessonCurrent = courseenrollGlobal.lessonCurrent + 1;

                    //$.ajax({
                    //    url: url + `/CourseEnrolls/${courseenrollGlobal.courseEnrollId}`,
                    //    type: "put",
                    //    data: JSON.stringify(courseenrollGlobal),
                    //    contentType: "application/json",
                    //    success: function (result, status, xhr) {
                    //        console.log(result);
                    //        if (status == 'success') {
                    //            let featureHtml = ``;
                    //            $.each(result.course.lessons, function (index, value) {
                    //                //$('#myList').append('<li>' + value + '</li>');
                    //                if (value.lessonNum <= result.lessonCurrent) {
                    //                    featureHtml += `<div class="feature d-flex flex-row align-items-center justify-content-start">
                    //                                    <div class="feature_title">
                    //                                    <i class="fa fa-file-text-o" aria-hidden="true"></i>
                    //                                    <span><a class="text" href='https://localhost:5000/courses/lesson?courseId=${courseId}&lessonNum=${value.lessonNum}'>${value.name}</a></span></div>
                    //                                    <div class="feature_text ml-auto">10:34</div>
                    //                                </div>`;
                    //                } else {
                    //                    featureHtml += `<div class="feature d-flex flex-row align-items-center justify-content-start">
                    //                                    <div class="feature_title">
                    //                                    <i class="fa fa-file-text-o" aria-hidden="true"></i>
                    //                                    <span class="text-black-50">${value.name}</span></div>
                    //                                    <div class="feature_text ml-auto">10:34</div>
                    //                                </div>`;
                    //                }

                    //            });
                    //            document.getElementById('listFeature').innerHTML = featureHtml;
                    //        }
                    //    },
                    //    error: function (xhr, status, error) {
                    //        console.log(xhr)
                    //    }
                    //});
                }
            }, 1000);
        }
    }
});
