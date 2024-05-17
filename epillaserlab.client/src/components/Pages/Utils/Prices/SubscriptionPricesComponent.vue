<template>
<div class="price-content" id="subscriptionPrices">
  <div id="back">
    <div class="first-block">
      <div class="block"></div>
    </div>
    <div class="second-block">
      <div class="block"></div>
      <div class="block"></div>
      <div class="block"></div>
    </div>
  </div>
  <div id="front">
    <div class="line" id="hederLine">
      <div class="first-block">
        <div class="block"></div>
      </div>
      <div class="second-block">
        <div class="block"><span class="header-line-col-seasons">3 сеанса</span></div>
        <div class="block"><span class="header-line-col-seasons">5 сеансов</span></div>
        <div class="block"><span class="header-line-col-seasons">7 сеансов</span></div>
      </div>
    </div>
    <div id="containerLines">
      <div id="scroll">
        <div class="line" v-for="line in prices">
          <div class="first-block">
            <div class="block">
              <div class="price-container">
                  <span class="header">{{line.name}}</span>
                  <span class="description">{{line.description}}</span>
              </div>
            </div>
          </div>
          <div class="second-block">
            <div class="block"><span class="line-col-seasons">{{line.prices[0].price}}</span></div>
            <div class="block"><span class="line-col-seasons">{{line.prices[1].price}}</span></div>
            <div class="block"><span class="line-col-seasons">{{line.prices[2].price}}</span></div>
          </div>
        </div>
      </div>
    </div> 
  </div>
</div>
</template>
<script lang="ts">
import { defineComponent } from 'vue';

interface PricesContent{
  colSeansons: Number,
  price: Number
}

interface LineContent{
  name: String,
  description: String,
  prices: Array<PricesContent>
}

export default defineComponent({
  props: {
    prices: {type: Array<LineContent>, required:true}
  }

})
</script>
<style>
.price-content{
  &#subscriptionPrices{

overflow: hidden;
position: relative;

#back, #front{
  position: absolute;
  width: 1040px;
  height: 415px; 
  display: flex;
  flex-wrap: wrap;

  .block{
    flex: 1;
    display: flex;
  }

  .first-block{
    margin-right: 50px;
    flex: 1;
    display: flex;
    flex-wrap: wrap;
  }

  .second-block{
    flex: 3;
    display: flex;
    flex-direction: row;
    margin-right: 25px;
  }

}

#front{
  flex-direction: column;

  .block{
    flex-direction: row;
    align-items: center;
    flex-wrap: wrap;
    flex: 1;
    display: flex;
  }

  .second-block .block:not(:nth-last-child(1)){
    margin-right: 61px;
  }

  #hederLine{
    flex: 1;

    display: flex;
    flex-wrap: wrap;
    margin-top: 20px;

    .header-line-col-seasons{
      flex: 1;
      display: flex;
      flex-direction: column;
      align-items: center;
      text-align: center;

      font-weight: bold;
      font-size: 20px;
      letter-spacing: 0%;
      text-transform: uppercase;
      color: white;

    }
  }

  #containerLines{
    flex: 6;
    position: relative;
    padding-top: 10px;
    overflow-y: scroll;

    #scroll{
      width: 100%;
      position: absolute;
      
      display: flex;
      flex-wrap: wrap;
      flex-direction: column;

      .line{
        display: flex;
        flex-wrap: wrap;
        flex-direction: row;
        margin-bottom: 15px;
        padding-bottom: 15px;
        

        .first-block{
          margin-right: 50px;
        }
        .second-block{
          margin-right: 10px;
        }

        &:not(:nth-last-child(1)){
          border-bottom: 1px solid #8A8099;
        }

        .price-container{
          flex: 1;
          display: flex;
          flex-direction: column;
          flex-wrap: wrap;
          align-items: start;
          word-break: normal;

          color: white;
          
          span{
            text-align: start;
            padding-left: 15px;
          }

          .header{
            font-weight: bold;
            font-size: 20px;
            flex: 1;
          }

          .description{
            font-size: 15px;
            font-weight: 400;
            flex: 2;
          }
        }
        
        .line-col-seasons{
          flex: 1;
          text-align: center;

          font-size: 20px;
          font-weight: bold;
          color: #F7EFB2;
        }
      }

    }
  }
}

#back{ 
  .block{
    background-color: #7D6A9A;
    border-radius: 10px;
  }

  .first-block{
    display: flex;
    flex-wrap: wrap;

    .block{
      margin-top: 78px;
    }
  }

  .second-block{
    display: flex;
    flex-wrap: wrap;
    

    .block{
      margin-top: 10px;

      &:not(:nth-last-child(1)){
        margin-right: 60px;
      }
    }
  }
}


}
}
</style>