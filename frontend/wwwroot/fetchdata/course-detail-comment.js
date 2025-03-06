const urll = 'http://localhost:5000/api';

//let options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };

// Init When Load pageconst initPage = () => {
const initPage = async () => {
    await pushDataOnLoad();
}
document.addEventListener("DOMContentLoaded", initPage);

// ---------------------------------- Call API ----------------------------------
async function getAllComment(courseId) {
    const callApi = async (urll) => {
        return (await fetch(urll)).json();
    }
    return await callApi(`${urll}/Comment/${courseId}`);
}

// -------------------------------------------------------------------------------
let coursesGlobal = [];
let categoriesGlobal = [];
let commentGlobal = [];

const urlParams = new URLSearchParams(window.location.search);
const courseId = urlParams.get('id');
console.log(courseId);

async function pushDataOnLoad() {



    getAllComment(courseId).then(comments => {
        commentGlobal = comments;
        const commentContainer = document.getElementById('comments_contain');
        comments.forEach(comment => {
            const ul = document.createElement('ul');
            ul.classList.add('comments_list');
            const li = document.createElement('li');
            
            li.innerHTML += `
                            <div class="comment_item d-flex flex-row align-items-start jutify-content-start">
															<div class="comment_image"><div><img src="/images/comment_2.jpg" alt=""></div></div>
															<div class="comment_content">
																<div class="comment_title_container d-flex flex-row align-items-center justify-content-start">
																	<div class="comment_author"><a href="#">${comment.userName}</a></div>
																	<div class="comment_rating"><div class="rating_r rating_r_4"><i></i><i></i><i></i><i></i><i></i></div></div>
																	<div class="comment_time ml-auto">${comment.createDate.substring(0,10)}</div>
																</div>
																<div class="comment_text">
																	<p>${comment.content}</p>
																</div>
																<div class="comment_extras d-flex flex-row align-items-center justify-content-start">
																	<div class="comment_extra comment_likes"><a href="#"><i class="fa fa-heart" aria-hidden="true"></i><span>${comment.replies.length}</span></a></div>
																	
															</div>
														</div>
                              `;
            if (comment.replies !== null) {
                comment.replies.forEach(reply => {
                    li.innerHTML += `<ul>
															<li>
																<div class="comment_item d-flex flex-row align-items-start jutify-content-start">
																	<div class="comment_image"><div><img src="/images/comment_2.jpg" alt=""></div></div>
																	<div class="comment_content">
																		<div class="comment_title_container d-flex flex-row align-items-center justify-content-start">
																			<div class="comment_author"><a href="#">${reply.userName}</a></div>
																			<div class="comment_rating"><div class="rating_r rating_r_4"><i></i><i></i><i></i><i></i><i></i></div></div>
																			<div class="comment_time ml-auto">${reply.createDate.substring(0, 10)}</div>
																		</div>
																		<div class="comment_text">
																			<p>${reply.content}</p>
																		</div>
																		<div class="comment_extras d-flex flex-row align-items-center justify-content-start">
																		</div>
																	</div>
																</div>
																
															</li>
														</ul>`
                });
            }
            ul.appendChild(li);
            commentContainer.appendChild(ul);
            

        });
    });

}




$(document).ready(function () {
    $('#commentForm').submit(function (event) {
        event.preventDefault();

        const Commentcontent = $('#commentContent').val();
        console.log(courseId);

        const data = {
            content: Commentcontent,
            courseId: courseId
        };

        $.ajax({
            type: "post",
            url: `http://localhost:5000/api/Comment`,
            data: JSON.stringify(data),
            contentType: "application/json",

            success: function (result, status, xhr) {
                location.reload();
                
            }
        });
    });

    
});
