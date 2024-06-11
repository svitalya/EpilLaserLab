import{d as b,s as y,u as w,i as l,_ as g,e as D,o as d,c,a as t,w as m,v as f,y as T,F as _,h as C,l as I,f as O,m as N,t as S,x as A}from"./index-BSdrrqya.js";import{D as j,I as $,O as F}from"./style-CIU-wI-C.js";const E=b({components:{DataTable:j,TableBody:$,TableHead:F},async beforeCreate(){await fetch("/api/masters?limit=9999",{headers:{"Content-Type":"application/json"},credentials:"include"}).then(async s=>{let e=await s.json();e.message=="OK"&&(this.masters=e.data.recs)}),await fetch(`/api/schedules/${this.id}`,{headers:{"Content-Type":"application/json"},credentials:"include"}).then(async s=>{let e=await s.json();e.message=="OK"&&(this.data.date=e.rec.date.replace("T00:00:00",""),this.data.masterId=e.rec.masterId)}),await fetch(`/api/intervals/${this.id}`,{headers:{"Content-Type":"application/json"},credentials:"include"}).then(async s=>{let e=await s.json();e.message=="OK"&&(this.intervals.intervalString=e.intervalsString)}),this.$forceUpdate()},setup(){const s=y(),e=s.currentRoute.value.params.id,o=w(),p=l([{masterId:0,fio:""}]),i=l({intervalString:""}),r=l({date:new Date().toJSON().slice(0,10),masterId:null});return{data:r,submitForm:async v=>{await fetch(`/api/schedules/${e}`,{method:"PUT",headers:{"Content-Type":"application/json"},credentials:"include",body:JSON.stringify(r)}).then(async n=>{const u=await n.json();u.message=="OK"?(o.success("Запись успешно изменена"),s.push({name:"dashboard.schedules"})):u.message=="DUPLICATION"?o.error("Дублирование записи"):u.message=="NOT CHANGED"?o.info("Внесите изменения"):u.message=="DATA NOT VALID"||u.status==400?o.error("Введите значения"):o.error("Ошибка при добавлении записи")}).catch(n=>s.push({name:"dashboard"}))},masters:p,intervals:i,id:e,setIntervals:async v=>{await fetch(`/api/intervals/${e}`,{method:"POST",headers:{"Content-Type":"application/json"},credentials:"include",body:`"${i.intervalString}"`}).then(async n=>{const u=await n.json();u.message=="OK"?o.success("Запись успешно изменена"):u.message=="DUPLICATION"?o.error("Дублирование записи"):u.message=="NOT CHANGED"?o.info("Внесите изменения"):u.message=="DATA NOT VALID"||u.status==400?o.error("Введите значения"):o.error("Ошибка при добавлении записи")}).catch(n=>console.log(n))}}}}),V=t("div",{class:"flex-column"},[t("h1",{class:"row h3"}," Редактировать запись расписания ")],-1),B={class:"col-3"},U={class:"row form-group mt-3"},J=t("label",{class:"h5"},"Дата",-1),k={class:"col-12"},K=["min"],L={class:"row form-group mt-3"},P=t("label",{class:"h5"},"Мастер",-1),H={class:"col-12"},M=["value"],x=t("div",{class:"row mt-4"},[t("button",{type:"submit",class:"btn btn-success me-3",style:{width:"120px"}},"Сохранить")],-1),G={class:"row form-group mt-5 col-6"},R=t("label",{class:"h5"},"Рабочие интервалы",-1),q={class:"col-12"},z={class:"row mt-4"};function Q(s,e,o,p,i,r){const h=D("router-link");return d(),c("div",null,[V,t("form",{onSubmit:e[2]||(e[2]=I((...a)=>s.submitForm&&s.submitForm(...a),["prevent"])),class:"row mt-3"},[t("div",B,[t("div",U,[J,t("div",k,[m(t("input",{type:"date","onUpdate:modelValue":e[0]||(e[0]=a=>s.data.date=a),class:"form-control",placeholder:"Введите название",min:new Date().toJSON().slice(0,10)},null,8,K),[[f,s.data.date]])])]),t("div",L,[P,t("div",H,[m(t("select",{class:"form-control","onUpdate:modelValue":e[1]||(e[1]=a=>s.data.masterId=a)},[(d(!0),c(_,null,C(s.masters,a=>(d(),c("option",{value:a.masterId},S(a.fio),9,M))),256))],512),[[T,s.data.masterId]])])]),x])],32),t("div",G,[R,t("div",q,[m(t("textarea",{type:"text","onUpdate:modelValue":e[3]||(e[3]=a=>s.intervals.intervalString=a),class:"form-control",placeholder:"Введите интервал"},null,512),[[f,s.intervals.intervalString]])]),t("div",z,[t("button",{type:"button",class:"btn btn-primary me-3",style:{width:"120px"},onClick:e[4]||(e[4]=(...a)=>s.setIntervals&&s.setIntervals(...a))},"Установить"),O(h,{tag:"button",to:{name:"dashboard.schedules"},type:"submit",class:"btn btn-secondary",style:{width:"120px"}},{default:N(()=>[A("Назад")]),_:1})])])])}const Y=g(E,[["render",Q]]);export{Y as default};
