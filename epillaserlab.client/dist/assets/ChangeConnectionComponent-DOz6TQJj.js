import{d as v,b as c,u as m,a as h,_ as y,o as D,c as _,e as s,w as d,v as l,m as b}from"./index-RBsaCAxW.js";const E=v({setup(){const t=c(),e=m(),o=h({server:null,database:null,user:null,password:null,port:null}),r=h({dumpBase64:null});return{data:o,submitForm:async()=>{await fetch("/api/db/change",{method:"POST",headers:{"Content-Type":"application/json"},credentials:"include",body:JSON.stringify(o)}).then(async n=>{const a=await n.json();a.message=="OK"?(t.success("Данные подключения к БД успешно изменены. Необходимо переавторизоваться"),o.server=a.data.server,o.database=a.data.database,o.user=a.data.user,o.password=a.data.password,o.port=a.data.port,e.push({name:"login-dashboard"})):a.message=="ERROR"?t.error("Выбранная база данных не является допустимой"):a.message=="DATA NOT VALID"&&t.error("Не удалось подключится к БД")}).catch(n=>e.push({name:"dashboard"}))},getDump:async()=>{await fetch("/api/db/dump",{method:"POST",headers:{"Content-Type":"application/json"},credentials:"include"}).then(async n=>{const a=await n.json();a.message=="OK"?window.open(a.dump,"_blank"):t.error("Произошла неизвестная ошибка")}).catch(n=>e.push({name:"dashboard"}))},getBase64:n=>{let a=n.files[0];var i=new FileReader;i.readAsDataURL(a),i.onload=function(){let p=i.result.split(";"),w=p[0];["data:application/octet-stream"].indexOf(w)!=-1?r.dumpBase64=p[1].replace("base64,",""):(n.value=null,t.error("Недопустимый тип файла"))},i.onerror=function(p){console.log("Error: ",p)}},submitRestoreDump:async()=>{if(!r.dumpBase64){t.info("Загрузите файл БД");return}await fetch("/api/db/restore",{method:"POST",headers:{"Content-Type":"application/json"},credentials:"include",body:JSON.stringify(r)}).then(async n=>{const a=await n.json();a.message=="OK"?(t.success("Данные БД успешно восстановлены. Необходимо переавторизоваться"),e.push({name:"login-dashboard"})):a.message=="ERROR"&&t.error("Выбранная база данных не является допустимой")}).catch(n=>e.push({name:"dashboard"}))}}},async mounted(){const t=c(),e=m();await fetch("/api/db/data",{method:"GET",headers:{"Content-Type":"application/json"},credentials:"include"}).then(async o=>{const r=await o.json();r.message=="OK"?(this.data.server=r.data.server,this.data.database=r.data.database,this.data.user=r.data.user,this.data.password=r.data.password,this.data.port=r.data.port):t.error("Произошла ошибка при получении текущих данных БД")}).catch(o=>e.push({name:"dashboard"}))}}),C={class:"row mt-3"},B={class:"row form-group mt-3"},F=s("label",{class:"h5"},"Сервер",-1),O={class:"col-12"},R={class:"row form-group mt-3"},T=s("label",{class:"h5"},"Название",-1),j={class:"col-12"},A={class:"row form-group mt-3"},S=s("label",{class:"h5"},"Логин",-1),V={class:"col-12"},U={class:"row form-group mt-3"},k=s("label",{class:"h5"},"Пароль",-1),K={class:"col-12"},N={class:"row form-group mt-3"},$=s("label",{class:"h5"},"Порт",-1),P={class:"col-12"},J=s("div",{class:"row form-group mt-4"},[s("button",{type:"submit",class:"btn btn-success",style:{width:"120px"}},"Сохранить")],-1),L={class:"col-3 ms-4"},M={class:"row form-group mt-3"},G=s("label",{class:"h5"},"Копия БД",-1),I={class:"input-group mb-3"},q=s("label",{class:"input-group-text"},"Загрузка",-1),z={class:"row form-group"},H=s("button",{type:"submit",class:"btn btn-success",style:{width:"120px"}},"Восстановить",-1);function Q(t,e,o,r,f,g){return D(),_("div",C,[s("form",{onSubmit:e[5]||(e[5]=b((...u)=>t.submitForm&&t.submitForm(...u),["prevent"])),class:"col-3"},[s("div",B,[F,s("div",O,[d(s("input",{type:"text","onUpdate:modelValue":e[0]||(e[0]=u=>t.data.server=u),class:"form-control",placeholder:"Введите адрес сервера"},null,512),[[l,t.data.server]])])]),s("div",R,[T,s("div",j,[d(s("input",{type:"text","onUpdate:modelValue":e[1]||(e[1]=u=>t.data.database=u),class:"form-control",placeholder:"Введите название"},null,512),[[l,t.data.database]])])]),s("div",A,[S,s("div",V,[d(s("input",{type:"text","onUpdate:modelValue":e[2]||(e[2]=u=>t.data.user=u),class:"form-control",placeholder:"Введите логин"},null,512),[[l,t.data.user]])])]),s("div",U,[k,s("div",K,[d(s("input",{type:"password","onUpdate:modelValue":e[3]||(e[3]=u=>t.data.password=u),class:"form-control",placeholder:"Введите пароль"},null,512),[[l,t.data.password]])])]),s("div",N,[$,s("div",P,[d(s("input",{type:"text","onUpdate:modelValue":e[4]||(e[4]=u=>t.data.port=u),class:"form-control",placeholder:"Введите порт"},null,512),[[l,t.data.port]])])]),J],32),s("div",L,[s("form",{onSubmit:e[8]||(e[8]=b((...u)=>t.submitRestoreDump&&t.submitRestoreDump(...u),["prevent"]))},[s("div",M,[G,s("div",I,[s("input",{type:"file",class:"form-control",onChange:e[6]||(e[6]=u=>{u.target.files.length>0&&t.getBase64(u.target)})},null,32),q])]),s("div",z,[H,s("button",{type:"button",onClick:e[7]||(e[7]=(...u)=>t.getDump&&t.getDump(...u)),class:"btn btn-secondary ms-3",style:{width:"120px"}},"Получить копию")])],32)])])}const Y=y(E,[["render",Q]]);export{Y as default};