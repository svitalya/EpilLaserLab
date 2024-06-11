import{d as _,s as v,u as w,r as C,i as b,_ as D,e as k,o as c,c as m,a as e,w as l,y as I,F as T,h as B,v as p,z as E,f as O,m as F,l as U,t as A,x as N}from"./index-BSdrrqya.js";import{D as V,I as j,O as $}from"./style-CIU-wI-C.js";const S=_({components:{DataTable:V,TableBody:j,TableHead:$},async beforeCreate(){await fetch("/api/branches?page=0&limit=999&order=name&sort=asc",{headers:{"Content-Type":"application/json"},credentials:"include"}).then(async s=>{let t=await s.json();t.message=="OK"&&(this.branches=t.data.recs)}),await fetch(`/api/masters/${this.id}`,{headers:{"Content-Type":"application/json"},credentials:"include"}).then(async s=>{let t=await s.json();if(t.message=="OK"){let o=t.rec;this.data.branchId=o.branchId,this.data.employee.surname=o.employee.surname,this.data.employee.name=o.employee.name,this.data.employee.patronymic=o.employee.patronymic,this.data.employee.isWork=o.employee.isWork,this.imageSrc=`/resources/images/${o.photoPath}`}}),this.$forceUpdate()},setup(){const s=v(),t=s.currentRoute.value.params.id,o=w();let h=C("https://imgholder.ru/300X300/8493a8/adb9ca&font=kelson");const f=b([{branchId:0,address:""}]),n=b({branchId:0,employee:{surname:null,name:null,patronymic:null,isWork:!0},photo:null});return{data:n,submitForm:async d=>{await fetch(`/api/masters/${t}`,{method:"PUT",headers:{"Content-Type":"application/json"},credentials:"include",body:JSON.stringify(n)}).then(async r=>{const u=await r.json();u.message=="OK"?(o.success("Запись успешно изменена"),s.push({name:"dashboard.masters"})):u.message=="DUPLICATION"||u.message=="DUPLICATION"?o.error("Дублирование записи"):u.message=="NOT CHANGED"?o.info("Внесите изменения"):u.message=="DATA NOT VALID"||u.status==400?o.error("Введите значения"):o.error("Ошибка при изменении записи")}).catch(r=>s.push({name:"dashboard"}))},getBase64:d=>{let r=d.files[0];var u=new FileReader;u.readAsDataURL(r),u.onload=function(){let i=u.result.split(";"),g=i[0];["data:image/jpeg","data:image/png","data:image/jpg"].indexOf(g)!=-1?(n.photo=i[1].replace("base64,",""),document.getElementById("image").src=u.result):(d.value=null,o.error("Недопустимый тип файла"))},u.onerror=function(i){console.log("Error: ",i)}},branches:f,imageSrc:h,id:t}}}),x=e("div",{class:"flex-column"},[e("h1",{class:"row h3"}," Изменить запись сотрудника ")],-1),L={class:"col-3"},W={class:"row form-group mt-3"},M=["src"],P=e("label",{class:"h5"},"Изображение",-1),R={class:"input-group mb-3"},J=e("label",{class:"input-group-text"},"Загрузка",-1),K={class:"row form-group mt-3"},H=e("label",{class:"h5"},"Филиал",-1),z={class:"col-12"},G=["value"],X={class:"row form-group mt-3"},q=e("label",{class:"h5"},"Фамилия",-1),Q={class:"col-12"},Y={class:"row form-group mt-3"},Z=e("label",{class:"h5"},"Имя",-1),ee={class:"col-12"},se={class:"row form-group mt-3"},te=e("label",{class:"h5"},"Отчество",-1),ae={class:"col-12"},oe={class:"form-check mt-3"},ue=e("label",{class:"form-check-label h5",for:"flexCheckDefault"}," Работает ",-1),le={class:"row mt-4"},ne=e("button",{type:"submit",class:"btn btn-success me-3",style:{width:"120px"}},"Сохранить",-1);function re(s,t,o,h,f,n){const y=k("router-link");return c(),m("div",null,[x,e("form",{onSubmit:t[6]||(t[6]=U((...a)=>s.submitForm&&s.submitForm(...a),["prevent"])),class:"row mt-3"},[e("div",L,[e("div",W,[e("img",{width:"300px",height:"300px",src:s.imageSrc,id:"image"},null,8,M),P,e("div",R,[e("input",{type:"file",class:"form-control",onChange:t[0]||(t[0]=a=>{a.target.files.length>0&&s.getBase64(a.target)})},null,32),J])]),e("div",K,[H,e("div",z,[l(e("select",{class:"form-control","onUpdate:modelValue":t[1]||(t[1]=a=>s.data.branchId=a)},[(c(!0),m(T,null,B(s.branches,a=>(c(),m("option",{value:a.branchId},A(a.address),9,G))),256))],512),[[I,s.data.branchId]])])]),e("div",X,[q,e("div",Q,[l(e("input",{type:"text","onUpdate:modelValue":t[2]||(t[2]=a=>s.data.employee.surname=a),class:"form-control",placeholder:"Введите адрес"},null,512),[[p,s.data.employee.surname]])])]),e("div",Y,[Z,e("div",ee,[l(e("input",{type:"text","onUpdate:modelValue":t[3]||(t[3]=a=>s.data.employee.name=a),class:"form-control",placeholder:"Введите адрес"},null,512),[[p,s.data.employee.name]])])]),e("div",se,[te,e("div",ae,[l(e("input",{type:"text","onUpdate:modelValue":t[4]||(t[4]=a=>s.data.employee.patronymic=a),class:"form-control",placeholder:"Введите адрес"},null,512),[[p,s.data.employee.patronymic]])])]),e("div",oe,[ue,l(e("input",{class:"form-check-input",type:"checkbox","onUpdate:modelValue":t[5]||(t[5]=a=>s.data.employee.isWork=a),id:"flexCheckDefault"},null,512),[[E,s.data.employee.isWork]])]),e("div",le,[ne,O(y,{tag:"button",to:{name:"dashboard.masters"},type:"submit",class:"btn btn-secondary",style:{width:"120px"}},{default:F(()=>[N("Назад")]),_:1})])])],32)])}const ce=D(S,[["render",re]]);export{ce as default};
