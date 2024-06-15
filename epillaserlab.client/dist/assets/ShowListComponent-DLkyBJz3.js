import{d as $,u as j,b as E,r as C,_ as O,g as h,o as N,c as K,h as d,p as i,y,t as I,e as D}from"./index-DnMKBQom.js";import{I as U,O as V,D as P}from"./style-ko_XgC6k.js";import{U as R}from"./UserStore-DUfprOyA.js";const x=$({components:{TableBody:U,TableHead:V,DataTable:P},setup(){const a=e=>{let t=e.replace("T00:00:00","").split("-");return t.reverse(),t.join(".")},r=j(),m=E(),b=C([]),o=C({}),s=C({}),c=e=>e.target.parentNode.dataset.id,g=async e=>{let t=c(e);await r.push({name:"dashboard.schedules.edit",params:{id:t}})},p=async e=>{if(!confirm("Вы точно хотите удалить запись?"))return;let u=c(e);await fetch(`/api/schedules/${u}`,{method:"DELETE",credentials:"include"}).then(async n=>{const l=await n.json();l.message=="OK"?(m.success("Запись удалена"),f(void 0)):l.message=="BLOCK"?m.error("Удаление записи приведет к потере данных"):m.error("Ошибка при удалении записи")}).catch(n=>r.push({name:"dashboard"}))},f=async e=>{let t,u,n,l;n=s.value.sort,typeof e=="string"?(t=o.value.page,u=o.value.limit,n=n=="asc"?"desc":"asc",l=e.split(":")[0],l!==s.value.order&&(n="desc")):typeof e==typeof{}?(t=e.page-1<0?0:e.page-1,u=e.per_page,l=s.value.order):(t=o.value.page,u=o.value.limit,l=s.value.order);let _={page:t,limit:u,sort:n,order:l,branchId:null};var k=await R.user();k.roleId==2&&(_.branchId=k.admin.branchId);var S=new URLSearchParams(_);await fetch(`/api/schedules?${S}`,{headers:{"Content-Type":"application/json"},credentials:"include"}).then(async w=>{let T=await w.json();if(T.message="OK"){let v=T.data,B=v.max;t=v.page;let L=v.recs;b.value=L,o.value={...o.value,page:t,total:B,limit:u},s.value={...s.value,sort:n,order:l}}else b.value=[],o.value={...o.value,page:0,total:0,limit:u},s.value={...s.value,sort:n,order:l}}).catch(w=>r.push({name:"dashboard"}))};return{tableData:b,pagination:o,loadData:f,sort:s,delClick:p,editClick:g,toDate:a}}}),z=x,F=D("TableBodyCell",{class:"d-flex justify-content-center",colspan:"2"}," Нет записей ",-1);function H(a,r,m,b,o,s){const c=h("table-head"),g=h("router-link"),p=h("table-body"),f=h("data-table");return N(),K("div",null,[d(f,{rows:a.tableData,pagination:a.pagination,sort:a.sort,striped:"",onLoadData:a.loadData,style:{"font-size":"large !important"}},{thead:i(()=>[d(c,{sortable:"master",onSorting:a.loadData},{default:i(()=>[y("Мастер")]),_:1},8,["onSorting"]),d(c,{sortable:"date",onSorting:a.loadData},{default:i(()=>[y("Дата")]),_:1},8,["onSorting"]),d(c,{style:{width:"15%"}},{default:i(()=>[d(g,{to:{name:"dashboard.schedules.add"},tag:"button",type:"button",class:"btn btn-success"},{default:i(()=>[y("Добавить")]),_:1})]),_:1})]),tbody:i(({row:e})=>[d(p,{textContent:I(e.master.fio)},null,8,["textContent"]),d(p,{textContent:I(a.toDate(e.date))},null,8,["textContent"]),d(p,{"data-id":e.scheduleId,style:{width:"15%"}},{default:i(()=>[D("button",{type:"button",class:"btn btn-primary me-2",onClick:r[0]||(r[0]=(...t)=>a.editClick&&a.editClick(...t))},"ИЗМЕНИТЬ"),D("button",{type:"button",class:"btn btn-danger",onClick:r[1]||(r[1]=(...t)=>a.delClick&&a.delClick(...t))},"УДАЛИТЬ")]),_:2},1032,["data-id"])]),empty:i(()=>[F]),_:1},8,["rows","pagination","sort","onLoadData"])])}const M=O(z,[["render",H]]);export{M as default};