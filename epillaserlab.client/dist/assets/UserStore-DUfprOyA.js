const t={async user(){var r=await(await fetch("/api/auth/user",{method:"GET",headers:{"Content-Type":"application/json"},credentials:"include"})).json();return r.message=="OK"?r.user:{}},isClient(e){return e&&e.roleId==3}};export{t as U};