$(function () {
    function initDragAndDrop() {
        $(".answer").draggable({
            revert: "invalid",
            containment: "body",
            zIndex: 100,
            helper: "clone",
            start: function (event, ui) {
                $(this).css("opacity", 0.5);
            },
            stop: function (event, ui) {
                $(this).css("opacity", 1);
            }
        });

        $(".dropbox").droppable({
            accept: ".answer",
            hoverClass: "dropbox-hover",
            drop: function (event, ui) {
                var questionno = $(this).data("questionno");
                $(`i[data-completeiconid="${questionno}"]`).removeClass("d-none");

                var droppedAnswer = ui.draggable.data("answer");
                var $dropbox = $(this);

                if ($dropbox.hasClass("filled")) {
                    //alert("Ô này đã có đáp án!");
                    return;
                }

                $dropbox.text(ui.draggable.text());
                $dropbox.append(`<button data-questionno=${questionno} class="clear-btn">x</button>`);
                $dropbox.addClass("filled");

                $dropbox.data("dropped-answer", droppedAnswer);
                ui.draggable.hide();
                $dropbox.find(".clear-btn").show();
            }
        });
    }

    // Gọi hàm initDragAndDrop lần đầu
    initDragAndDrop();

    // Đăng ký sự kiện khi nội dung được render lại
    $(document).on("contentRendered", function () {
        initDragAndDrop();
    });

    // Nút xóa đáp án
    $(document).on("click", ".clear-btn", function () {
        var $dropbox = $(this).closest(".dropbox");
        var droppedAnswer = $dropbox.data("dropped-answer");

        var questionno = $(this).data("questionno"); console.log(questionno)
        $(`i[data-completeiconid="${questionno}"]`).addClass("d-none");

        $(".answer").filter(function () {
            return $(this).data("answer") == droppedAnswer;
        }).show();

        $dropbox.text("").removeClass("filled").removeData("dropped-answer");

        
    });

    // Nút nộp bài
    $("#submit").on("click", function () {
        let isCorrect = true;

        $(".dropbox").each(function () {
            var correctAnswer = $(this).data("answer");
            var droppedAnswer = $(this).data("dropped-answer");

            if (correctAnswer != droppedAnswer) {
                isCorrect = false;
                alert("Sai đáp án ở ô: " + $(this).text());
            }
        });

        if (isCorrect) {
            alert("Chúc mừng! Bạn đã trả lời đúng tất cả.");
        } else {
            alert("Bạn vẫn có đáp án chưa chính xác!");
        }
    });
});
