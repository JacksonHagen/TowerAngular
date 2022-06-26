using Tower.Interfaces;

namespace Tower.Models
{
	public class Ticket : IDbItem
	{
		public int Id { get; set; }
		public string CreatorId { get; set; }
		public Profile Creator { get; set; }
		public string EventId { get; set; }


	}
}