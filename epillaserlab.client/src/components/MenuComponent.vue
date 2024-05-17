<template>
  <div>
    <div class="menu vertical" v-if="vertical">
      <ul>
        <li @click="click">
          <svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" viewBox="0 0 30 30" :hide="short">
            <path d="M27.6764 0H2.32427C1.04307 0 0 1.52704 0 3.4017V5.47081C0 7.34564 1.04307 8.87252 2.32427 8.87252H27.6764C28.9576 8.87252 30 7.34548 30 5.47081V3.4017C30.0001 1.52704 28.9576 0 27.6764 0ZM28.788 5.47081C28.788 6.36867 28.2898 7.09801 27.6765 7.09801H2.32427C1.71097 7.09801 1.21211 6.36867 1.21211 5.47081V3.4017C1.21211 2.50385 1.71086 1.7745 2.32427 1.7745H27.6764C28.2897 1.7745 28.7879 2.50385 28.7879 3.4017L28.788 5.47081Z"/>
            <path d="M27.6764 10.4803H2.32427C1.04307 10.4803 0 12.0073 0 13.8822V15.9513C0 17.8261 1.04307 19.353 2.32427 19.353H27.6764C28.9576 19.353 30 17.8259 30 15.9513V13.8822C30.0001 12.0073 28.9576 10.4803 27.6764 10.4803ZM28.788 15.9513C28.788 16.8491 28.2898 17.5785 27.6765 17.5785H2.32427C1.71097 17.5785 1.21211 16.8491 1.21211 15.9513V13.8822C1.21211 12.9843 1.71086 12.255 2.32427 12.255H27.6764C28.2897 12.255 28.7879 12.9843 28.7879 13.8822L28.788 15.9513Z"/>
            <path d="M27.6764 21.1275H2.32427C1.04307 21.1275 0 22.6545 0 24.5292V26.5983C0 28.4731 1.04307 30 2.32427 30H27.6764C28.9576 30 30 28.473 30 26.5983V24.5292C30.0001 22.6545 28.9576 21.1275 27.6764 21.1275ZM28.788 26.5983C28.788 27.4962 28.2898 28.2255 27.6765 28.2255H2.32427C1.71097 28.2255 1.21211 27.4962 1.21211 26.5983V24.5292C1.21211 23.6313 1.71086 22.902 2.32427 22.902H27.6764C28.2897 22.902 28.7879 23.6313 28.7879 24.5292L28.788 26.5983Z"/>
          </svg>
          <svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" viewBox="0 0 30 30" :hide="!short">
            <g clip-path="url(#clip0_231_263)">
            <path d="M17.6757 15L29.8036 2.87276C30.0646 2.6114 30.0646 2.18791 29.8036 1.92655L28.0739 0.195942C27.9484 0.070792 27.7778 0 27.6005 0C27.4229 0 27.2525 0.070792 27.1271 0.195942L14.9995 12.3235L2.87196 0.195942C2.62071 -0.0553062 2.17605 -0.0549902 1.92543 0.195942L0.196082 1.92655C-0.0652793 2.18791 -0.0652793 2.6114 0.196082 2.87276L12.3236 15L0.196082 27.1272C-0.0652793 27.3886 -0.0652793 27.8121 0.196082 28.0734L1.92575 29.8041C2.05121 29.9292 2.22155 30 2.39917 30C2.57678 30 2.74681 29.9292 2.87227 29.8041L14.9998 17.6765L27.1274 29.8041C27.2528 29.9292 27.4235 30 27.6008 30C27.7781 30 27.9488 29.9292 28.0742 29.8041L29.8039 28.0734C30.0649 27.8121 30.0649 27.3886 29.8039 27.1272L17.6757 15Z"/>
            </g>
            <defs>
            <clipPath id="clip0_231_263">
            <rect width="30" height="30" fill="white"/>
            </clipPath>
            </defs>
          </svg>
        </li>

        <li :hide="hideIcon && hideText" @click="$emit('scrollToComponent', 'main')" :class="{'active': pageId == 'main'}">
          <svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" viewBox="0 0 30 30" :hide="hideIcon">
            <g clip-path="url(#clip0_231_139)">
            <path fill-rule="evenodd" clip-rule="evenodd" d="M17.6979 1.08915L28.8121 10.1818L28.8693 10.2448C29.0424 10.435 29.3075 10.7382 29.5319 11.0998C29.7459 11.4449 30 11.9638 30 12.5718V27.6895C30 28.963 28.9694 30 27.6923 30H20.4924C19.5366 30 18.7617 29.2251 18.7617 28.2692V22.8391C18.7617 20.7632 17.0836 19.0852 15.0194 19.0852C12.9553 19.0852 11.2771 20.7632 11.2771 22.8391V28.2692C11.2771 29.2251 10.5023 30 9.54639 30H2.30769C1.03059 30 0 28.963 0 27.6895V12.3952C0 11.6424 0.418835 11.0354 0.657531 10.734C0.796719 10.5583 0.93315 10.4151 1.0342 10.3158C1.08542 10.2655 1.12935 10.2248 1.16238 10.195C1.17894 10.18 1.19289 10.1677 1.20381 10.1583L1.2179 10.1461L1.22316 10.1416L1.22628 10.139C1.22628 10.139 1.22718 10.1382 1.97019 11.021L1.22718 10.1382L1.23868 10.1285L12.264 1.09174C13.7829 -0.362909 16.1776 -0.364052 17.6979 1.08915ZM2.71438 11.903L2.70785 11.9087C2.69623 11.9192 2.67638 11.9376 2.65107 11.9624C2.59905 12.0135 2.5314 12.0849 2.46651 12.1668C2.39961 12.2513 2.35154 12.327 2.32355 12.3858C2.31532 12.4032 2.31044 12.4156 2.30769 12.4235V27.6895L2.30776 27.6914L2.3085 27.6923H8.96947V22.8391C8.96947 19.494 11.6755 16.7775 15.0194 16.7775C18.3633 16.7775 21.0693 19.494 21.0693 22.8391V27.6923H27.6915L27.6923 27.6895V12.5718C27.6923 12.5757 27.6932 12.576 27.6923 12.5718C27.6884 12.5552 27.6678 12.4725 27.5709 12.3164C27.474 12.1603 27.3467 12.0043 27.2169 11.8583L16.1585 2.81129L16.1208 2.7743C15.4878 2.15174 14.4751 2.15221 13.8427 2.77539L13.8051 2.81243L2.71438 11.903Z"/>
            </g>
            <defs>
            <clipPath id="clip0_231_139">
            <rect width="30" height="30" fill="white"/>
            </clipPath>
            </defs>
          </svg>
          <a :hide="hideText"> Главная</a>
        </li>
        <li :hide="hideIcon && hideText" @click="$emit('scrollToComponent', 'price')" :class="{'active': pageId == 'price'}">
          <svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" viewBox="0 0 30 30" :hide="hideIcon">
            <path d="M24.375 2.72727H22.5V1.36364C22.5 1.00198 22.3025 0.655131 21.9508 0.3994C21.5992 0.143668 21.1223 0 20.625 0H9.375C8.87772 0 8.40081 0.143668 8.04918 0.3994C7.69754 0.655131 7.5 1.00198 7.5 1.36364V2.72727H5.625C4.13316 2.72727 2.70242 3.15828 1.64752 3.92547C0.592632 4.69267 0 5.73321 0 6.81818V25.9091C0 26.9941 0.592632 28.0346 1.64752 28.8018C2.70242 29.569 4.13316 30 5.625 30H24.375C25.8668 30 27.2976 29.569 28.3525 28.8018C29.4074 28.0346 30 26.9941 30 25.9091V6.81818C30 5.73321 29.4074 4.69267 28.3525 3.92547C27.2976 3.15828 25.8668 2.72727 24.375 2.72727ZM11.25 2.72727H18.75V5.45455H11.25V2.72727ZM26.25 25.9091C26.25 26.2707 26.0525 26.6176 25.7008 26.8733C25.3492 27.1291 24.8723 27.2727 24.375 27.2727H5.625C5.12772 27.2727 4.65081 27.1291 4.29918 26.8733C3.94754 26.6176 3.75 26.2707 3.75 25.9091V6.81818C3.75 6.45652 3.94754 6.10968 4.29918 5.85395C4.65081 5.59821 5.12772 5.45455 5.625 5.45455H7.5V6.81818C7.5 7.17984 7.69754 7.52669 8.04918 7.78242C8.40081 8.03815 8.87772 8.18182 9.375 8.18182H20.625C21.1223 8.18182 21.5992 8.03815 21.9508 7.78242C22.3025 7.52669 22.5 7.17984 22.5 6.81818V5.45455H24.375C24.8723 5.45455 25.3492 5.59821 25.7008 5.85395C26.0525 6.10968 26.25 6.45652 26.25 6.81818V25.9091ZM22.5 12.2727C22.5 12.6344 22.3025 12.9812 21.9508 13.237C21.5992 13.4927 21.1223 13.6364 20.625 13.6364H9.375C8.87772 13.6364 8.40081 13.4927 8.04918 13.237C7.69754 12.9812 7.5 12.6344 7.5 12.2727C7.5 11.9111 7.69754 11.5642 8.04918 11.3085C8.40081 11.0528 8.87772 10.9091 9.375 10.9091H20.625C21.1223 10.9091 21.5992 11.0528 21.9508 11.3085C22.3025 11.5642 22.5 11.9111 22.5 12.2727ZM22.5 17.7273C22.5 18.0889 22.3025 18.4358 21.9508 18.6915C21.5992 18.9472 21.1223 19.0909 20.625 19.0909H9.375C8.87772 19.0909 8.40081 18.9472 8.04918 18.6915C7.69754 18.4358 7.5 18.0889 7.5 17.7273C7.5 17.3656 7.69754 17.0188 8.04918 16.763C8.40081 16.5073 8.87772 16.3636 9.375 16.3636H20.625C21.1223 16.3636 21.5992 16.5073 21.9508 16.763C22.3025 17.0188 22.5 17.3656 22.5 17.7273ZM22.5 23.1818C22.5 23.5435 22.3025 23.8903 21.9508 24.1461C21.5992 24.4018 21.1223 24.5455 20.625 24.5455H9.375C8.87772 24.5455 8.40081 24.4018 8.04918 24.1461C7.69754 23.8903 7.5 23.5435 7.5 23.1818C7.5 22.8202 7.69754 22.4733 8.04918 22.2176C8.40081 21.9618 8.87772 21.8182 9.375 21.8182H20.625C21.1223 21.8182 21.5992 21.9618 21.9508 22.2176C22.3025 22.4733 22.5 22.8202 22.5 23.1818Z"/>
          </svg>
          <a :hide="hideText">Услуги</a>
        </li>
        <!-- <li :hide="hideIcon && hideText"  @click="$emit('scrollToComponent', 'price')" :class="{'active': pageId == 'main'}">
          <svg xmlns="http://www.w3.org/2000/svg" width="30" height="31" viewBox="0 0 30 31" :hide="hideIcon">
            <g clip-path="url(#clip0_231_160)">
            <path d="M15 14.7786C16.8233 14.7786 18.5719 14.0255 19.8612 12.6851C21.1505 11.3448 21.8748 9.5268 21.8748 7.6312C21.8748 5.73561 21.1505 3.91765 19.8612 2.57727C18.5719 1.23688 16.8233 0.483856 15 0.483856C13.1767 0.483856 11.4281 1.23688 10.1388 2.57727C8.84953 3.91765 8.12523 5.73561 8.12523 7.6312C8.12523 9.5268 8.84953 11.3448 10.1388 12.6851C11.4281 14.0255 13.1767 14.7786 15 14.7786ZM15 17.9188C5.85955 17.9188 0 23.1628 0 25.7161V30.4839H30V25.7161C30 22.6284 24.4527 17.9188 15 17.9188Z"/>
            </g>
            <defs>
            <clipPath id="clip0_231_160">
            <rect width="30" height="30" fill="white" transform="translate(0 0.485321)"/>
            </clipPath>
            </defs>
          </svg>
          <a :hide="hideText">Кабинет</a>
        </li> -->
      </ul>
    </div>
    <div class="menu horizontal" v-if="!vertical">
      <ul>
        <li @click="$emit('scrollToComponent', 'main')"><a :class="{'active': pageId == 'main'}">Главная</a></li>
        <li @click="$emit('scrollToComponent', 'price')"><a :class="{'active': pageId == 'price'}">Услуги</a></li>
        <!-- <li @click="$emit('scrollToComponent', 'cabinet')"><a :class="{'active': pageId == 'cabinet'}">Кабинет</a></li> -->
      </ul>
    </div>
  </div>
</template>
<script lang="ts">

import { defineComponent, ref} from "vue";

export default defineComponent({
  name: "men",
  props: {
    show: {type: Boolean, required: false},
    vertical: {type: Boolean, required: true},
    pageId: {type: String, required: true}
  },

  watch: { 
    show: function(newVal, oldVal) { // watch it
      
      if(this.short !== newVal) this.click(null, newVal);
    }
  },

  setup(){
    const short = ref(false);
    const hideText = ref(true);
    const hideIcon = ref(true);

    return {short, hideText, hideIcon}
  },

  methods:{
    click(_: MouseEvent|null = null, show: boolean|undefined = undefined){
      if(show == undefined){
        this.$emit('shortCloseClick', this.pageId);
      }

      this.short = show === undefined ? !this.short : (show as boolean);
      setTimeout(() => {
        
        if(this.short){
          this.hideIcon = !this.hideIcon
          setTimeout(() => this.hideText = !this.hideText, 1)
        }else{
          this.hideText = !this.hideText
          setTimeout(() => this.hideIcon = !this.hideIcon, 1)
        }

      }, 1)

    }
  }
})
</script>
<style>
.menu > ul li {
  cursor: pointer;
}

.menu.horizontal{
  width: 460px;
  height: auto;
  display: flex;
  flex-direction: row;
}

.menu > ul{
  margin: 0;
  padding: 0;
  list-style-type: none;
}

.menu.horizontal ul{

  width: 100%;
  display: flex;
  flex-direction: row;

  flex-wrap: initial;
}

.menu.horizontal ul li{
  flex: 1;
  align-items: center;
  display: inline;
  text-align: center;
}

.menu.horizontal ul li a{
  font-size: 20px;
  font-weight: bold;
  line-height: auto;
  letter-spacing: 0;
  color: #F7EFB2;
  text-decoration: none;
}

.menu.horizontal ul li a:hover, .menu.horizontal ul li a.active{
  color: #9C7638 !important;
  text-decoration: none;
  border-bottom: 1px #9C7638 solid;
} 

.menu.vertical ul {
  background-color: #4D396A;
  padding: 15px;
  border-radius: 10px;
  display: inline-flex;
  flex-direction: column;
  width: auto;
}

.menu.vertical ul li{
  display: flex;
  flex-direction: row;
  align-items: center;

  color: #F7EFB2;
  fill: #F7EFB2;
}

.menu.vertical ul li:hover, .menu.vertical ul li.active{
  color: #9C7638;
  fill: #9C7638;
  text-decoration: none;
}

.menu.vertical ul li[hide="false"]:nth-child(2){
  margin-top: 20px;
}

.menu.vertical ul li[hide="false"]:not(:last-child){
  margin-bottom: 20px;
}

.menu.vertical ul li[hide="true"]{
  display: none;
}


.menu.vertical ul li svg[hide="true"]{
  display: none;
}

.menu.vertical ul li a{
  letter-spacing: 0;
  line-height: 100%;
  font-size: 20px;
  font-weight: bold;
  margin: 2px 0 2px 10px;
}

.menu.vertical ul li a[hide="true"]{
  display: none;
}
</style>