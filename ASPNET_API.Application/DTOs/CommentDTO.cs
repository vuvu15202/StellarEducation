using System.ComponentModel.DataAnnotations;

namespace ASPNET_API.Models.DTO
{
    public class CreateComment
    {
        [Required]
        [MaxLength(200)]
        public string Content { get; set; }
        public int CourseId { get; set; }
    }

    public class CreateReply
    {
        [Required]
        [MaxLength(200)]
        public string Content { get; set; }
        public int CommentId { get; set; }
    }

    public class CommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string userName { get; set; }
        public DateTime CreateDate { get; set; }

        public ICollection<ReplyDTO>? Replies { get; set; }

    }


    public class ReplyDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public int CommentId { get; set; }
    }
}
