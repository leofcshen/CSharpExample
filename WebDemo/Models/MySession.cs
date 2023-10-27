namespace WebDemo.Models {
	public class MySession {
		private readonly IHttpContextAccessor _contextAccessor;
		public MySession(IHttpContextAccessor contextAccessor) {
			_contextAccessor = contextAccessor;
		}

		public string Name {
			get { return new SessionAccessor(_contextAccessor).Get<string>(); }
			set { new SessionAccessor(_contextAccessor).Set(value, nameof(Name)); }
		}
	}
}
