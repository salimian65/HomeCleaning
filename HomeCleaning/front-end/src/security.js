import Oidc from "oidc-client";

// var mgr = new Oidc.UserManager({
//     authority: 'http://192.168.168.172:8086',
//     client_id: 'frontend',
//     redirect_uri: 'http://192.168.168.172:8085/callback',
//     response_type: 'code',
//     scope: 'openid profile backend',
//     post_logout_redirect_uri: 'http://192.168.168.172:8085',
//     userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }),
// })

var mgr = new Oidc.UserManager({
  authority: "http://localhost:5000",
  client_id: "frontend",
  redirect_uri: "http://localhost:8080/callback",
  response_type: "code",
  scope: "openid profile offline_access backend",
  post_logout_redirect_uri: "http://localhost:8080",
  userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }),
  automaticSilentRenew: true,
  silent_redirect_uri: "http://localhost:8080/static/silent-renew.html",
  accessTokenExpiringNotificationTime: 60,
});

Oidc.Log.logger = console;
Oidc.Log.level = Oidc.Log.INFO;

export default mgr;
