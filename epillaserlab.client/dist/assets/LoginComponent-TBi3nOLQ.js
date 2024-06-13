import{d as c,a as p,u as g,b as f,_ as v,o as b,c as h,e,m,w as n,v as u}from"./index-BabCA39_.js";const y=c({name:"Login",setup(){const s=p({name:"",phone:"",email:"",login:"",password:""}),t=p({login:"",password:""}),l=g(),i=f(),a=async()=>{s.email||(s.email=null),fetch("/api/auth/register/clients",{method:"POST",headers:{"Content-Type":"application/json"},credentials:"include",body:JSON.stringify(s)}).then(async o=>{let r=await o.json();r.message=="OK"?(t.login=s.login,t.password=s.password,await d()):r.message=="BLOCK"&&i.warning("Данный логин уже занят")})},d=async()=>{fetch("/api/auth/login",{method:"POST",headers:{"Content-Type":"application/json"},credentials:"include",body:JSON.stringify(s)}).then(async o=>{let r=await o.json();r.message=="OK"?(i.success("Вход выполнен"),l.push({name:"home"})):r.message=="INVALID CREDENTIALS"&&i.warning("Пользователя с таким логином и паролем нет")})};return{dataRegister:s,dataLogin:t,submitRegister:a,toast:i,submitLogin:d}},mounted(){const s=document.querySelectorAll(".tab"),t=document.querySelectorAll(".tab-content");s.forEach(l=>{l.addEventListener("click",()=>{s.forEach(a=>a.classList.remove("active")),l.classList.add("active");const i=l.getAttribute("data-tab");t.forEach(a=>{a.classList.remove("active"),a.id===i&&a.classList.add("active")})})})}}),w={id:"loginForm"},L={class:"container"},E=e("div",{class:"tabs"},[e("div",{class:"tab active","data-tab":"login"},"Вход"),e("div",{class:"tab","data-tab":"register"},"Регистрация")],-1),R={class:"tab-content active",id:"login"},B={class:"form-group"},D={class:"form-group"},S=e("button",{class:"btn"},"Вход",-1),V={class:"tab-content",id:"register"},C={class:"form-group"},F={class:"form-group"},O={class:"form-group"},T={class:"form-group"},U={class:"form-group"},A=e("button",{class:"btn",type:"submit"},"Регистрация",-1);function N(s,t,l,i,a,d){return b(),h("div",w,[e("div",L,[E,e("div",R,[e("form",{onSubmit:t[2]||(t[2]=m((...o)=>s.submitLogin&&s.submitLogin(...o),["prevent"]))},[e("div",B,[n(e("input",{type:"text","onUpdate:modelValue":t[0]||(t[0]=o=>s.dataLogin.login=o),class:"form-control",placeholder:"Логин"},null,512),[[u,s.dataLogin.login]])]),e("div",D,[n(e("input",{type:"password","onUpdate:modelValue":t[1]||(t[1]=o=>s.dataLogin.password=o),class:"form-control",placeholder:"Пароль"},null,512),[[u,s.dataLogin.password]])]),S],32)]),e("div",V,[e("form",{onSubmit:t[8]||(t[8]=m((...o)=>s.submitRegister&&s.submitRegister(...o),["prevent"]))},[e("div",C,[n(e("input",{type:"text","onUpdate:modelValue":t[3]||(t[3]=o=>s.dataRegister.name=o),class:"form-control",placeholder:"Имя"},null,512),[[u,s.dataRegister.name]])]),e("div",F,[n(e("input",{type:"text","onUpdate:modelValue":t[4]||(t[4]=o=>s.dataRegister.phone=o),class:"form-control",placeholder:"Телефон"},null,512),[[u,s.dataRegister.phone]])]),e("div",O,[n(e("input",{type:"text","onUpdate:modelValue":t[5]||(t[5]=o=>s.dataRegister.login=o),class:"form-control",placeholder:"Логин"},null,512),[[u,s.dataRegister.login]])]),e("div",T,[n(e("input",{type:"password","onUpdate:modelValue":t[6]||(t[6]=o=>s.dataRegister.password=o),class:"form-control",placeholder:"Пароль"},null,512),[[u,s.dataRegister.password]])]),e("div",U,[n(e("input",{type:"email","onUpdate:modelValue":t[7]||(t[7]=o=>s.dataRegister.email=o),class:"form-control",placeholder:"Электронная почта"},null,512),[[u,s.dataRegister.email]])]),A],32)])])])}const I=v(y,[["render",N]]);export{I as default};
