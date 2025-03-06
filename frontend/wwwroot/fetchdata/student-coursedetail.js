// 1. Khai báo các biến và cấu hình chung
const API_BASE_URL = getCookie("apiurl") || "http://localhost:5000/api"; 
const COURSE_ID = new URL(window.location.href).searchParams.get('courseId') || 0;
//const urlParams = new URLSearchParams(window.location.search);
//const courseId = urlParams.get('id');

// 2. Các hàm tiện ích chung
function handleAjaxError(xhr, status, error) {
	console.error("AJAX Error:", error);
	console.error("Status:", status);
	console.error("Response:", xhr.responseText);
	if (JSON.parse(xhr.responseText) != undefined) alert(xhr.responseText);
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
	getCourse: function (successCallback) {
        sendRequest("GET", `/Courses/${COURSE_ID}`, {}, successCallback);
	},

	getRelatedCourse: function (successCallback) {
		sendRequest("GET", `/Courses/RelatedCourse/${COURSE_ID}`, {}, successCallback);
	},

	searchCourses: function (name, categoryId, successCallback) {
		sendRequest("GET", `/Courses/search?name=${name}&categoryId=${categoryId}`, {}, successCallback);
	},
};



// 4. Sự kiện liên quan đến giao diện (UI)
$(document).ready(function () {
    ApiService.getCourse(function (data) {
        displayCourseDetail(data);

    });

	ApiService.getRelatedCourse(function (data) {
		renderCourses(data);

    });

    function displayCourseDetail(courseDetail) {
        const courseTitleElement = document.getElementById('course_title');
        const courseImageElement = document.getElementById('course_image');
        const courseDesTitleElement = document.getElementById('course_des_title');
        const courseDesElement = document.getElementById('course_description');
        const courseLessonElement = document.getElementById('course_lesson_title');
        const courseLessonsList = document.getElementById('course_lessons_list');
        const courseCategoryElement = document.getElementById('course_category_info');
		const coursePriceElement = document.getElementById('course_price');
        const enrollButton = document.getElementById('enroll_button');
        const numberEnroll = document.getElementById('numberEnrolled');
        const numberLesson = document.getElementById('numberLesson');
        const lecturerName = document.getElementById('lecturerName');
        const categoryElement = document.getElementById('category_name');
		const numberTime = document.getElementById('numberTime');
		const lecturerInfo = document.getElementById('lecturerInfo');

        //const language = document.getElementById('language');
        //const updatedAt = document.getElementById('updatedAt');


        courseTitleElement.textContent = courseDetail.name;
        courseDesTitleElement.textContent = courseDetail.name;
        courseDesElement.textContent = courseDetail.description;
        courseLessonElement.textContent = courseDetail.name;

        courseImageElement.src = courseDetail.image || 'images/default_course.jpg';
        courseImageElement.alt = courseDetail.name;

        numberEnroll.textContent = courseDetail.numberEnrolled;
        numberLesson.textContent = courseDetail.lessons.length;
		numberTime.textContent = formatTime(courseDetail.totalTime);
		lecturerName.textContent = courseDetail.lecturer.lastName + " " + courseDetail.lecturer.firstName;

        //category name
        categoryElement.textContent = courseDetail.category.name;


		//lecturer info
		let lecturer = courseDetail.lecturer;
		lecturerInfo.innerHTML = `
			<div class="sidebar_section_title">Thông tin giáo viên</div>
			<div class="sidebar_teacher">
				<div class="teacher_title_container d-flex flex-row align-items-center justify-content-start">
					<div class="teacher_image"><img style="height:inherit;" src="${lecturer.image}" alt=""></div>
					<div class="teacher_title" style="margin-bottom: 40px">
						<div class="teacher_name"><a href="javascript:void(0)">${lecturer.lastName} ${lecturer.firstName}</a></div>
						<div class="teacher_position">${lecturer.email}</div>
					</div>
				</div>

			</div>
			<div class="teacher_info" style="margin-bottom: 50px">
				<p>${lecturer.description}.</p>
			</div>
		`



        courseLessonsList.innerHTML = '';

        if (courseDetail.lessons.length === 0) {
            const noLessonsMessage = document.createElement('div');
            noLessonsMessage.textContent = 'No lessons available for this course.';
            courseLessonsList.appendChild(noLessonsMessage);
        } else {
            courseDetail.lessons.forEach(lesson => {
                const lessonItem = document.createElement('div');
                lessonItem.classList.add('dropdown_item');

                const lessonTitle = document.createElement('div');
                lessonTitle.classList.add('dropdown_item_title');
                lessonTitle.innerHTML = `<span>Lesson ${lesson.lessonNum}: ${lesson.name}</span>`;

                const lessonText = document.createElement('div');
                lessonText.classList.add('dropdown_item_text');
                lessonText.innerHTML = `<p>${lesson.description}</p>`;

                lessonItem.appendChild(lessonTitle);
                lessonItem.appendChild(lessonText);

                courseLessonsList.appendChild(lessonItem);
            });
        }

		coursePriceElement.innerHTML = courseDetail.price != 0 ? '<a href="/Home/contact"><button class="btn btn-warning">Liên hệ</button></a>' : '<div class="text-danger" style="font-size: 24px;">Miễn phí</div>';
		coursePriceElement.classList.add('text-danger');

        enrollButton.addEventListener('click', async () => {
            window.location.href = `lesson?courseId=${courseDetail.courseId}&lessonNum=1`;
        });
    }

	// Render courses on the page
	function renderCourses(coursesToDisplay) {
		const coursesList = document.getElementById('courses_list');
		coursesList.innerHTML = '<h2 class="col-lg-12 my-3">Khóa học liên quan</h2>';

		coursesToDisplay.forEach(course => {

			const courseCol = document.createElement('div');
			courseCol.classList.add('col-lg-3', 'course_col');

			const courseDiv = document.createElement('div');
			courseDiv.classList.add('course','py-0');

			const courseImageDiv = document.createElement('div');
			courseImageDiv.classList.add('course_image');
			const courseImage = document.createElement('img');
			courseImage.src = course.image || 'images/default_course.jpg';
			courseImage.alt = course.name;
			courseImage.style.width = '100%';
			courseImageDiv.appendChild(courseImage);

			const courseBodyDiv = document.createElement('div');
			courseBodyDiv.classList.add('course_body','pt-1');

			const courseTitle = document.createElement('h3');
			courseTitle.classList.add('course_title');
			const courseTitleLink = document.createElement('a');
			courseTitleLink.href = `/Courses/Detail?courseId=${course.courseId}`;
			courseTitleLink.textContent = course.name;
			courseTitle.appendChild(courseTitleLink);
			courseBodyDiv.appendChild(courseTitle);

			const courseFooterDiv = document.createElement('div');
			courseFooterDiv.classList.add('course_footer');

			const courseFooterContentDiv = document.createElement('div');
			courseFooterContentDiv.classList.add('course_footer_content', 'd-flex', 'flex-row', 'align-items-center', 'justify-content-start');

			const courseNumberOfEnrolled = document.createElement('div');
			courseNumberOfEnrolled.classList.add('course_infoo');
			courseNumberOfEnrolled.innerHTML = `<i class="fa fa-graduation-cap mr-2 text-warning" aria-hidden="true"></i><span>${course.numberEnrolled} đang học</span>`;
			courseFooterContentDiv.appendChild(courseNumberOfEnrolled);

			const coursePrice = document.createElement('div');
			coursePrice.classList.add('course_price', 'ml-auto');
			coursePrice.innerHTML = course.price != 0 ? '<a class="text-danger" href="/Home/contact">Liên hệ</a>' : 'Miễn phí';
			courseFooterContentDiv.appendChild(coursePrice);

			courseFooterDiv.appendChild(courseFooterContentDiv);

			courseDiv.appendChild(courseImageDiv);
			courseDiv.appendChild(courseBodyDiv);
			courseDiv.appendChild(courseFooterDiv);

			courseCol.appendChild(courseDiv);
			coursesList.appendChild(courseCol);
		});
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

});