document.addEventListener('DOMContentLoaded', function () {
    const API_BASE_URL = getCookie("apiurl") || "http://localhost:5000/api";

    var token = getCookie("token");
    fetch(`${decodeURIComponent(API_BASE_URL)}/Student/GetCourseByUserId`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + token
        }
    })
    .then(response => response.json())
    .then(data => {
        const authorsTableBody = document.getElementById('authors-table-body');
        const projectsCardBody = document.getElementById('projects-card-body');

        data.forEach(item => {
            const authorRow = document.createElement('tr');
            authorRow.innerHTML = `
            <td><a href="javascript:void(0)" class="course-name" data-course-id="${item.courseId}" data-enroll-id="${item.id}">${item.courseName}</a></td>
            <td>${item.enrollDate}</td>
            <td style="color:red;">${item.endDate}</td>
            <td class="text-center"><span class="badge badge-sm bg-gradient-${item.courseStatus === 1 ? 'success' : 'secondary'}">${item.courseStatus === 1 ? 'Completed' : 'Not Completed'}</span></td>
            <td class="">
                        <div>
                            <button class="btn btn-light" type="button" id="ellipsisMenu" data-bs-toggle="dropdown" aria-expanded="false">
                                <i class="fas fa-ellipsis-v"></i>
                            </button>
                            <ul class="dropdown-menu" aria-labelledby="ellipsisMenu">
                                <li>
                                    <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4 course-name" data-course-id="${item.courseId}" data-enroll-id="${item.id}">
                                        <i class="fas fa-file-pdf text-lg me-1"></i>
                                        Xem chi tiết
                                    </button>
                                    </hr>
                                </li>
                            </ul>
                        </div>
                    </td>
            `;
            authorsTableBody.appendChild(authorRow);
        });

        // Handle course name click
        document.querySelectorAll('.course-name').forEach(courseNameElement => {
            courseNameElement.addEventListener('click', function () {
                const courseId = this.getAttribute('data-course-id');
                const enrollId = this.getAttribute('data-enroll-id');

                // Fetch the course detail for the selected course
                fetch(`${decodeURIComponent(API_BASE_URL)}/Student/GetCourseDetailById?courseEnrollId=${enrollId}`)
                    .then(response => response.json())
                    .then(item => {
                        // Clear the previous content
                        projectsCardBody.innerHTML = '';

                        const quizScores = item.quiz;
                        const quizScoresArray = quizScores ? quizScores.split(';') : null;

                        const projectCard = document.createElement('div');
                        projectCard.classList.add('col-12', 'mb-4');
                        projectCard.innerHTML = `
                                    <div class="card course-detail" data-course-id="${courseId}">
                                        <div class="card-body">
                                            <h5 class="card-title">${item.courseName}</h5>
                                            <p class="card-text"><strong>Enroll Date:</strong> ${item.enrollDate}</p>
                                            <p class="card-text"><strong>End Date:</strong> ${item.endDate}</p>
                                            <p class="card-text"><strong>Average Grade:</strong> ${item.averageGrade !== null ? item.averageGrade : 'N/A'}</p>
                                            <p class="card-text"><strong>Student Fee ID:</strong> ${item.studentFeeId !== null ? item.studentFeeId : 'N/A'}</p>
                                            <p class="card-text"><strong>Quiz Scores:</strong></p>
                                            <ul>
                                                            ${item.examCandidates ? item.examCandidates.map((score, index) => `<li><a target="_blank" href="/ielts/quiz?examcandidateid=${score.examCandidateId}">Quiz ${index + 1}: ${score.overall}</a></li>`).join('') : '<li>Quiz scores: N/A</li>'}
                                            </ul>
                                            <input type="hidden" id="studentId" value="${item.studentId}">
                                            <input type="hidden" id="courseId" value="${item.courseId}">
                                            <div class="text-center mt-3">
                                                ${item.courseStatus === 100 ? `<button class="btn btn-primary me-3" onclick="exportData(${item.courseId})">Export</button>` : ''}
                                                <a href="javascript:void(0)" class="btn btn-info" onclick="redirectToCourseDetails(${item.courseId})">Xem khoá học</a>
                                            </div>
                                        </div>
                                    </div>
                                `;
                        projectsCardBody.appendChild(projectCard);

                        document.getElementById('authors-col').style.width = '65%';
                        document.getElementById('projects-col').style.width = '35%';
                        document.getElementById('authors-col').style.float = 'left';
                        document.getElementById('projects-col').style.float = 'right';

                        document.getElementById('projects-col').style.display = 'block';
                    })
                    .catch(error => console.error('Error fetching course details:', error));
            });
        });
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
// Function to handle export button click
function exportData(courseId) {
    var studentId = document.getElementById(`studentId`).value;
    console.log('Export button clicked. Student ID:', studentId);
    var token = getCookie("token");
    $.ajax({
        url: `${decodeURIComponent(API_BASE_URL)}/ExportPDF/generate/${studentId}/${courseId}`,
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
            a.download = "certificate.pdf"; // Tên file bạn muốn tải về
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
}

// Function to handle redirect button click
function redirectToCourseDetails(courseId) {
    var hiddenCourseId = document.getElementById(`courseId`).value;
    window.location.href = `/Courses/Detail?courseId=${hiddenCourseId}`;
}