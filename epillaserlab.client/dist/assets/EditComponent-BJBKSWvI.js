import{d as v,s as f,u as b,i as p,_ as y,e as _,o as r,c as l,a as s,w as i,y as D,F as w,h as g,v as d,f as C,m as T,l as I,t as E,x as F}from"./index-BSdrrqya.js";import{D as O,I as k,O as N}from"./style-CIU-wI-C.js";const V=v({components:{DataTable:O,TableBody:k,TableHead:N},async beforeCreate(){await fetch("/api/services?page=0&limit=999&order=name&sort=asc",{headers:{"Content-Type":"application/json"},credentials:"include"}).then(async e=>{let t=await e.json();t.message=="OK"&&(this.serviceData=t.data.recs)}),await fetch(`/api/seasontickets/${this.id}`,{headers:{"Content-Type":"application/json"},credentials:"include"}).then(async e=>{let t=await e.json();if(t.message=="OK"){let o=t.rec;this.data.serviceId=o.serviceId,this.data.sessions=o.sessions,this.data.validityPeriod=o.validityPeriod,this.data.price=o.price}}),this.$forceUpdate()},setup(){const e=f(),t=e.currentRoute.value.params.id,o=b(),n=p({serviceId:null,sessions:null,validityPeriod:null,price:null});let c=p([{serviceId:0,name:""}]);return{data:n,submitForm:async m=>{await fetch(`/api/seasontickets/${t}`,{method:"PUT",headers:{"Content-Type":"application/json"},credentials:"include",body:JSON.stringify(n)}).then(async u=>{const a=await u.json();a.message=="OK"?(o.success("Запись успешно добавлена"),e.push({name:"dashboard.seasontickets"})):a.message=="DUPLICATION"?o.error("Дублирование записи"):a.message=="NOT CHANGED"?o.info("Внесите изменения"):a.message=="DATA NOT VALID"||a.status==400?o.error("Введите значения"):o.error("Ошибка при изменении записи")}).catch(u=>e.push({name:"dashboard"}))},serviceData:c,id:t}}}),A=s("div",{class:"flex-column"},[s("h1",{class:"row h3"}," Изменить запись абонемента ")],-1),B={class:"col-3"},P={class:"row form-group mt-3"},U=s("label",{class:"h5"},"Услуга",-1),$={class:"col-12"},j=["value"],S={class:"row form-group mt-3"},J=s("label",{class:"h5"},"Посещений",-1),K={class:"col-12"},L={class:"row form-group mt-3"},M=s("label",{class:"h5"},"Дней действует",-1),H={class:"col-12"},R={class:"row form-group mt-3"},G=s("label",{class:"h5"},"Цена",-1),q={class:"col-12"},z={class:"row mt-4"},Q=s("button",{type:"submit",class:"btn btn-success me-3",style:{width:"120px"}},"Сохранить",-1);function W(e,t,o,n,c,h){const m=_("router-link");return r(),l("div",null,[A,s("form",{onSubmit:t[4]||(t[4]=I((...u)=>e.submitForm&&e.submitForm(...u),["prevent"])),class:"row mt-3"},[s("div",B,[s("div",P,[U,s("div",$,[i(s("select",{class:"form-control","onUpdate:modelValue":t[0]||(t[0]=u=>e.data.serviceId=u)},[(r(!0),l(w,null,g(e.serviceData,u=>(r(),l("option",{value:u.serviceId},E(u.name),9,j))),256))],512),[[D,e.data.serviceId]])])]),s("div",S,[J,s("div",K,[i(s("input",{type:"number","onUpdate:modelValue":t[1]||(t[1]=u=>e.data.sessions=u),class:"form-control",placeholder:"Введите количество посещений"},null,512),[[d,e.data.sessions]])])]),s("div",L,[M,s("div",H,[i(s("input",{type:"number","onUpdate:modelValue":t[2]||(t[2]=u=>e.data.validityPeriod=u),class:"form-control",placeholder:"Введите количество дней"},null,512),[[d,e.data.validityPeriod]])])]),s("div",R,[G,s("div",q,[i(s("input",{type:"number","onUpdate:modelValue":t[3]||(t[3]=u=>e.data.price=u),class:"form-control",placeholder:"Введите цену"},null,512),[[d,e.data.price]])])]),s("div",z,[Q,C(m,{tag:"button",to:{name:"dashboard.seasontickets"},type:"submit",class:"btn btn-secondary",style:{width:"120px"}},{default:T(()=>[F("Назад")]),_:1})])])],32)])}const Z=y(V,[["render",W]]);export{Z as default};
