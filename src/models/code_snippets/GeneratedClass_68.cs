routes.MapRoute("SecureReceivePack", "{project}.git/git-receive-pack",
new { controller = "Git", action = "SecureReceivePack" },
new { method = new HttpMethodConstraint("POST") });