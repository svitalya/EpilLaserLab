import{d as u,a as p,u as c,b as m,_ as b,o as f,c as h,e as o,w as i,v as l,m as w}from"./index-1Nx-P1XQ.js";const g=u({name:"Login",setup(){const s=p({login:"",password:""}),e=c(),a=m();return{data:s,submit:async()=>{fetch("/api/auth/login",{method:"POST",headers:{"Content-Type":"application/json"},credentials:"include",body:JSON.stringify(s)}).then(async r=>{let n=await r.json();n.message=="OK"?fetch("/api/dashboard",{method:"GET",headers:{"Content-Type":"application/json"},credentials:"include"}).then(async t=>{(await t.json()).message=="OK"?await e.push({name:"dashboard"}):a.error("Вход в панель не разрешен")}):n.message=="INVALID CREDENTIALS"?a.error("Нет пользователя с таким логином и паролем"):a.error("Ошибка при авторизации")})},toast:a}}}),y={class:"text-center"},_={class:"form-outline mb-4"},v=o("label",{class:"form-label",for:"login"},"Логин",-1),E={class:"form-outline mb-4"},T=o("label",{class:"form-label",for:"password"},"Пароль",-1),C=o("button",{type:"submit",class:"btn btn-primary btn-block mb-4"},"Войти",-1);function B(s,e,a,d,r,n){return f(),h("div",y,[o("form",{style:{width:"22rem"},class:"position-absolute top-50 start-50 translate-middle",onSubmit:e[2]||(e[2]=w((...t)=>s.submit&&s.submit(...t),["prevent"]))},[o("div",_,[v,i(o("input",{"onUpdate:modelValue":e[0]||(e[0]=t=>s.data.login=t),type:"text",id:"login",class:"form-control",required:""},null,512),[[l,s.data.login]])]),o("div",E,[T,i(o("input",{"onUpdate:modelValue":e[1]||(e[1]=t=>s.data.password=t),type:"password",id:"password",class:"form-control",required:""},null,512),[[l,s.data.password]])]),C],32)])}const J=b(g,[["render",B]]);export{J as default};