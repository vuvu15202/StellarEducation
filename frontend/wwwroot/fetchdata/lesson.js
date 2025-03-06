const API_BASE_URL = getCookie("apiurl") || "http://localhost:5000/api";
const courseId = new URL(window.location.href).searchParams.get('courseId') || 1;
const lessonNum = new URL(window.location.href).searchParams.get('lessonNum') || 1;
var videoTime = 1;
let options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };

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
async function getProjectById(id) {
    const callApi = async (API_BASE_URL) => {
        return (await fetch(API_BASE_URL)).json();
    }
    return await callApi(`${decodeURIComponent(API_BASE_URL)}/project/${id}`);
}

async function getTopDonateById(id) {
    const callApi = async (API_BASE_URL) => {
        return (await fetch(API_BASE_URL)).json();
    }
    return await callApi(`${decodeURIComponent(API_BASE_URL)}/project/${id}/donatesTop/5`);
}

async function getTotalAmountProject(id) {
    const callApi = async (API_BASE_URL) => {
        return (await fetch(API_BASE_URL)).json();
    }
    return await callApi(`${decodeURIComponent(API_BASE_URL)}/projects/${id}/amount`);
}

async function getCourseById(id) {
    const callApi = async (API_BASE_URL) => {
        return (await fetch(API_BASE_URL)).json();
    }
    return await callApi(`${decodeURIComponent(API_BASE_URL)}/CourseEnrolls/getcourseenroll/${id}`);
}
// -------------------------------------------------------------------------------
let courseenrollGlobal = {};
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

async function pushDataOnLoad() {
    const container = document.getElementById('projectContainer');
    const topDonateContainer = document.getElementById('topDonate');
    const listFeatureContainer = document.getElementById('listFeature');
    var token = getCookie("token"); 

    $.ajax({
        url: `${decodeURIComponent(API_BASE_URL)}/CourseEnrolls/getcourseenroll/${courseId}`,
        type: "get",
        headers: {
            "Authorization": "Bearer " + token,
        },
        contentType: "application/json",
        success: function (result, status, xhr) {
            var courseenroll = result;
            courseenrollGlobal = courseenroll;
            let course = courseenroll.course;
            $("#coursename").append(`<a href="javascript:void(0)" onclick="redirectDetail()">${course.name}</a>`);

            if (!(course.lessons.length > 0)) {
                toastr.error("Bài giảng này chưa sẵn sàng, vui lòng quay lại sau!", 'Notification', { timeOut: 3000 });
            }
            //const [amountData] = await Promise.all([getTotalAmountProject(projectId)]);
            let html = `<div class="course_container">
                <div class="course_title">${course.lessons[lessonNum - 1].name}</div>
                <div class="w-100" id="player"></div>
                <div class="course_tabs_container">
                    <div class="tabs d-flex flex-row align-items-center justify-content-start">
						    <div class="tab active">Description</div>
						    <div class="tab">Quiz</div>
					    </div>
                    <div class="tab_panels">
                        <div class="tab_panel active">
                            ${course.lessons[lessonNum - 1].description}
                        </div>
                        <div class="tab_panel tab_panel_2" id="btn-do-quiz">
                            <ul>
                                {{quiz}}
                            </ul>
                        </div>
                    </div>

                </div>
            `;
            let lessons = course.lessons;
            let lesson = course.lessons.find(obj => obj.lessonNum == lessonNum);
            let featureHtml = ``;
            $.each(lessons, function (index, value) {
                //$('#myList').append('<li>' + value + '</li>');
                if (value.lessonNum <= courseenroll.lessonCurrent) {
                    featureHtml += `<div class="feature d-flex flex-row align-items-center justify-content-start">
                        <div class="feature_title">
                        <i class="fa fa-file-text-o" aria-hidden="true"></i>
                        <span><a class="text" href='/courses/lesson?courseId=${courseId}&lessonNum=${value.lessonNum}'>${value.name}</a></span></div>
                        <div class="feature_text ml-auto">${formatTime(value.videoTime * 60)}</div>
                    </div>`;
                } else {
                    featureHtml += `<div class="feature d-flex flex-row align-items-center justify-content-start">
                        <div class="feature_title">
                        <i class="fa fa-file-text-o" aria-hidden="true"></i>
                        <span class="text-black-50">${value.name}</span></div>
                        <div class="feature_text ml-auto">${formatTime(value.videoTime * 60)}</div>
                    </div>`;
                }

            });

            //let quiz = '';
            //if (lesson.quizes && lesson.quizes.length > 0) {
            //    $.each(lesson.quizes, function (index, value) {
            //        quiz += `<li class="row mb-3">
            //            <div class="col-md-12">${value.question}</div>
            //            <div class="col-md-6"><input type="radio" name="question-${courseId}-${lesson.lessonId}-${value.questionNo}" value="A" />A. ${value.answerA}</div>
            //            <div class="col-md-6"><input type="radio" name="question-${courseId}-${lesson.lessonId}-${value.questionNo}" value="B" />B. ${value.answerB}</div>
            //            <div class="col-md-6"><input type="radio" name="question-${courseId}-${lesson.lessonId}-${value.questionNo}" value="C" />C. ${value.answerC}</div>
            //            <div class="col-md-6"><input type="radio" name="question-${courseId}-${lesson.lessonId}-${value.questionNo}" value="D" />D. ${value.answerD}</div>
            //        </li>`;
            //    });
            //    quiz += '<div class="d-flex justify-content-center"><button class="btn btn-info " id="btn-submit-quiz">Chấm điểm</button></div>';
                
            //} 
            //html = html.replace("{{quiz}}", quiz);
            container.innerHTML += html;
            listFeatureContainer.innerHTML += featureHtml;
            videoTime = lesson.videoTime; 
            onYouTubeIframeAPIReady(course.lessons[lessonNum - 1].videoUrl);
            initTabs();

            //render button quiz
            $.ajax({
                url: `${decodeURIComponent(API_BASE_URL)}/CourseEnrolls/getExamCandidate/${courseId}/${lessonNum}`,
                type: "get",
                headers: {
                    "Authorization": "Bearer " + token,
                },
                contentType: "application/json",
                success: function (result, status, xhr) {
                    if (result == 0) {
                        $("#btn-do-quiz").html(`
                            Bài giảng này hiện chưa có bài kiểm tra!
                        `);
                    } else {
                        $("#btn-do-quiz").html(`
                            <a href="/ielts/quiz?examcandidateid=${result}">
                                <button type="button" class="btn btn-warning">Do Quiz</button>
                            </a>
                        `);
                    }
                },
                error: function (xhr, status, error) {
                    console.log(xhr)
                }
            });
            $("#sidebar_section").removeClass("d-none");

        },
        error: function (xhr, status, error) {
            console.log(xhr)
        }
    });

    
    function initTabs() {
        if ($('.tab').length) {
            $('.tab').on('click', function () {
                $('.tab').removeClass('active');
                $(this).addClass('active');
                var clickedIndex = $('.tab').index(this);

                var panels = $('.tab_panel');
                panels.removeClass('active');
                $(panels[clickedIndex]).addClass('active');
            });
        }
        $("#btn-submit-quiz").click(function (e) {
            let requestData = [];
            // Lấy tất cả các radio được chọn
            $('ul input[type="radio"]:checked').each(function () {
                requestData.push($(this).attr('name') +'-'+ $(this).val());
            });
            console.log(JSON.stringify(requestData));
            $.ajax({
                url: url + '/courses/grade',
                type: "post",
                data: JSON.stringify(requestData),
                contentType: "application/json",
                success: function (result, status, xhr) {
                    if (status == 'success') {
                        alert('Kết quả của bạn là :' + result.result);
                    }
                    //var str = "<tr><td>" + result["id"] + "</td><td>" + result["name"] + "</td><td>" + result["startLocation"] + "</td><td>" + result["endLocation"] + "</td></tr>";
                    //$("table tbody").append(str);
                    //$("#resultDiv").show();
                },
                error: function (xhr, status, error) {
                    console.log(xhr)
                }
            });
        });
    };


    var player;

    // This function creates an <iframe> (and YouTube player)
    // after the API code downloads.
    function onYouTubeIframeAPIReady(videoId) {
        player = new YT.Player('player', {
            height: '390',
            width: '640',
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
                if (currentTime > 0.75*videoTime*60 && lessonNum == courseenrollGlobal.lessonCurrent) {
                    delete courseenrollGlobal['course'];
                    delete courseenrollGlobal['user'];
                    courseenrollGlobal.lessonCurrent = courseenrollGlobal.lessonCurrent + 1;

                    $.ajax({
                        url: `${decodeURIComponent(API_BASE_URL)}/CourseEnrolls/${courseenrollGlobal.courseEnrollId}`,
                        type: "put",
                        headers: {
                            "Authorization": "Bearer " + token,
                        },
                        data: JSON.stringify(courseenrollGlobal),
                        contentType: "application/json",
                        success: function (result, status, xhr) {
                            if (status == 'success') {
                                let featureHtml = ``;
                                $.each(result.course.lessons, function (index, value) {
                                    //$('#myList').append('<li>' + value + '</li>');
                                    if (value.lessonNum <= result.lessonCurrent) {
                                        featureHtml += `<div class="feature d-flex flex-row align-items-center justify-content-start">
                                                        <div class="feature_title">
                                                        <i class="fa fa-file-text-o" aria-hidden="true"></i>
                                                        <span><a class="text" href='/courses/lesson?courseId=${courseId}&lessonNum=${value.lessonNum}'>${value.name}</a></span></div>
                                                        <div class="feature_text ml-auto">${formatTime(value.videoTime * 60)}</div>
                                                    </div>`;
                                    } else {
                                        featureHtml += `<div class="feature d-flex flex-row align-items-center justify-content-start">
                                                        <div class="feature_title">
                                                        <i class="fa fa-file-text-o" aria-hidden="true"></i>
                                                        <span class="text-black-50">${value.name}</span></div>
                                                        <div class="feature_text ml-auto">${formatTime(value.videoTime * 60)}</div>
                                                    </div>`;
                                    }

                                });
                                document.getElementById('listFeature').innerHTML = featureHtml;
                            }
                        },
                        error: function (xhr, status, error) {
                            console.log(xhr)
                        }
                    });
                }
            }, 1000);
        }
    }

    function formatTime(seconds) {
        const days = Math.floor(seconds / (24 * 3600));
        seconds %= 24 * 3600;
        const hours = Math.floor(seconds / 3600);
        seconds %= 3600;
        const minutes = Math.floor(seconds / 60);
        const remainingSeconds = seconds % 60;

        const dayPart = days > 0 ? `${days}d ` : "";
        const hourPart = hours > 0 ? `${hours}h ` : "";
        const minutePart = minutes > 0 ? `${minutes}m ` : "";
        const secondPart = `${remainingSeconds}s`;

        return `${dayPart}${hourPart}${minutePart}${secondPart}`.trim();
    }

}



