// 1. Khai báo các biến và cấu hình chung
const API_BASE_URL = getCookie("apiurl") || "http://localhost:5000/api";
//const EXAM_CANDIDATE_ID = new URL(window.location.href).searchParams.get('examcandidateid') || 0;

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
	getPublicQuestionBanks: function (successCallback) {
		sendRequest("GET", `/QuestionBanks/public`, {}, successCallback);
	},

	getCategories: function (successCallback) {
		sendRequest("GET", "/Categories", {}, successCallback);
	},

	searchCourses: function (name, categoryId, successCallback) {
		sendRequest("GET", `/Courses/search?name=${name}&categoryId=${categoryId}`, {}, successCallback);
	},
};



// 4. Sự kiện liên quan đến giao diện (UI)
$(document).ready(function () {
	const searchForm = document.getElementById('courses_search_form');
	const searchInput = document.getElementById('search_input');
	const categorySelect = document.getElementById('courses_search_select');
	const coursesList = document.getElementById('courses_list');
	const paginationList = document.getElementById('pagination_list');
	const coursesShowing = document.getElementById('courses_showing');
	const coursesTotal = document.getElementById('courses_total');
	const coursesPerPage = 8;
	let currentPage = 1;
	let courses = [];


	ApiService.getPublicQuestionBanks(function (data) {
		courses = data;
		renderCourses(currentPage);
		createPaginationControls();
	});

	//ApiService.getCategories(function (data) {
	//	$.each(data, function (index, category) {
	//		const option = document.createElement('option');
	//		option.value = category.categoryId;
	//		option.textContent = category.name;
	//		categorySelect.appendChild(option);
	//	});

	//});

	// Form submit event
	//searchForm.addEventListener('submit', async (event) => {
	//	event.preventDefault();
	//	currentPage = 1;
	//	const name = searchInput.value.trim();
	//	const categoryId = categorySelect.value;
	//	ApiService.searchCourses(name, categoryId, function (data) {
	//		courses = data;
	//		renderCourses(currentPage);
	//		createPaginationControls();

	//	});


	//});


	// Render courses on the page
	function renderCourses(page) {
		coursesList.innerHTML = '';
		const start = (page - 1) * coursesPerPage;
		const end = start + coursesPerPage;
		const coursesToDisplay = courses.slice(start, end);

		coursesToDisplay.forEach(course => {

			const courseCol = document.createElement('div');
			courseCol.classList.add('col-lg-3','col-md-4','col-sm-6', 'course_col');

			const courseDiv = document.createElement('div');
			courseDiv.classList.add('course');

	

			const courseBodyDiv = document.createElement('div');
			courseBodyDiv.classList.add('course_body');

			const courseTitle = document.createElement('h3');
			courseTitle.classList.add('course_title');
			const courseTitleLink = document.createElement('div');
			courseTitleLink.textContent = course.examCode;
			courseTitle.appendChild(courseTitleLink);
			courseBodyDiv.appendChild(courseTitle);

			const courseFooterDiv = document.createElement('div');
			courseFooterDiv.classList.add('course_footer');

			const courseFooterContentDiv = document.createElement('div');
			courseFooterContentDiv.classList.add('course_footer_content', 'd-flex', 'flex-row', 'align-items-center', 'justify-content-start');


			const coursePrice = document.createElement('a');
			coursePrice.classList.add('course_price', 'ml-auto');
			coursePrice.href = `/ielts/test?questionbankid=${course.questionBankId}`;
			coursePrice.textContent = `Bắt đầu`;
			coursePrice.target = "_blank";
			courseFooterContentDiv.appendChild(coursePrice);

			courseFooterDiv.appendChild(courseFooterContentDiv);

			courseDiv.appendChild(courseBodyDiv);
			courseDiv.appendChild(courseFooterDiv);

			courseCol.appendChild(courseDiv);
			coursesList.appendChild(courseCol);
		});

		const totalResults = courses.length;
		coursesShowing.textContent = `${start + 1}-${Math.min(end, totalResults)}`;
		coursesTotal.textContent = totalResults;
	}

	// Create pagination controls
	function createPaginationControls() {
		paginationList.innerHTML = '';
		const totalPages = Math.ceil(courses.length / coursesPerPage);

		for (let i = 1; i <= totalPages; i++) {
			const pageItem = document.createElement('li');
			if (i === currentPage) {
				pageItem.classList.add('active');
			}
			const pageLink = document.createElement('a');
			pageLink.href = '#';
			pageLink.textContent = i;
			pageLink.addEventListener('click', (event) => {
				event.preventDefault();
				currentPage = i;
				renderCourses(currentPage);
				createPaginationControls();
			});
			pageItem.appendChild(pageLink);
			paginationList.appendChild(pageItem);
		}

		if (currentPage < totalPages) {
			const nextItem = document.createElement('li');
			const nextLink = document.createElement('a');
			nextLink.href = '#';
			nextLink.innerHTML = '<i class="fa fa-angle-right" aria-hidden="true"></i>';
			nextLink.addEventListener('click', (event) => {
				event.preventDefault();
				currentPage++;
				renderCourses(currentPage);
				createPaginationControls();
			});
			nextItem.appendChild(nextLink);
			paginationList.appendChild(nextItem);
		}
	}

});

document.addEventListener('DOMContentLoaded', async () => {
	
});