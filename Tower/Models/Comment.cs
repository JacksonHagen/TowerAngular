using Tower.Interfaces;

namespace Tower.Models
{
	public class Comment : IDbItem
	{
		public int Id { get; set; }
		public string CreatorId { get; set; }
		public Profile Creator { get; set; }
		public string Body { get; set; }
		public bool IsAttending { get; set; }
	}
}