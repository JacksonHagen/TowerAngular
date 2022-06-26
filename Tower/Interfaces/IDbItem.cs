using Tower.Models;

namespace Tower.Interfaces
{
	public interface IDbItem
	{
		int Id { get; set; }
		string CreatorId { get; set; }
		Profile Creator { get; set; }
	}
}