//
                //
                //
                // skill active and show function
                //
                //
                //
                var x = document.getElementById("sk1_act");
                x.addEventListener("click", function() {
                var divData1=document.getElementById("sk_now");
                divData1.innerText=document.getElementById("sk1_name").innerText
                });

                var x = document.getElementById("sk2_act");
                x.addEventListener("click", function() {
                var divData1=document.getElementById("sk_now");
                divData1.innerText=document.getElementById("sk2_name").innerText
                });
                
                var x = document.getElementById("sk3_act");
                x.addEventListener("click", function() {
                var divData1=document.getElementById("sk_now");
                divData1.innerText=document.getElementById("sk3_name").innerText
                });
                var x = document.getElementById("sk4_act");
                x.addEventListener("click", function() {
                var divData1=document.getElementById("sk_now");
                divData1.innerText=document.getElementById("sk4_name").innerText
                });
                var x = document.getElementById("sk5_act");
                x.addEventListener("click", function() {
                var divData1=document.getElementById("sk_now");
                divData1.innerText=document.getElementById("sk5_name").innerText
                });
                var x = document.getElementById("sk6_act");
                x.addEventListener("click", function() {
                var divData1=document.getElementById("sk_now");
                divData1.innerText=document.getElementById("sk6_name").innerText
                });
                var x = document.getElementById("sk7_act");
                x.addEventListener("click", function() {
                var divData1=document.getElementById("sk_now");
                divData1.innerText=document.getElementById("sk7_name").innerText
                });
                var x = document.getElementById("sk8_act");
                x.addEventListener("click", function() {
                var divData1=document.getElementById("sk_now");
                divData1.innerText=document.getElementById("sk8_name").innerText
                });
             
               

                //general selector

                var cnt = parseInt(document.getElementById("ptNum").innerText);

                var pt8 = parseInt(document.getElementById("p8").innerText);
                var pt7 = parseInt(document.getElementById("p7").innerText);
                var pt6 = parseInt(document.getElementById("p6").innerText);
                var pt5 = parseInt(document.getElementById("p5").innerText);
                var pt4 = parseInt(document.getElementById("p4").innerText);
                var pt3 = parseInt(document.getElementById("p3").innerText);
                var pt2 = parseInt(document.getElementById("p2").innerText);
                var pt1 = parseInt(document.getElementById("p1").innerText);
                

                // , {once : true}
                
                

                //skill and upgrade line performance detail

                //sk1

                var x = document.getElementById("sk1");                
                x.addEventListener("click", function() {
                if(cnt>=pt1) {
                cnt=cnt-pt1;
                var x = document.getElementById("sk1");
                x.disabled=true;
                var y = document.getElementById("sk2");
                y.disabled=false;
                //var z = document.getElementById("sk5");
                //z.disabled=false;
                //var v = document.getElementById("sk8");
                //v.disabled=false;

                var act = document.getElementById("sk1_act");
                act.disabled=false;    

                var e0 = document.querySelector('#up1_2');
                e0.style.backgroundColor = "white";
                var e1 = document.querySelector('#up1_5');
                e1.style.borderColor = "white";
                var e2 = document.querySelector('#up1_8');
                e2.style.borderColor = "white";
	            }
                var divData=document.getElementById("ptNum");
                divData.innerText=cnt;            
                });


                 //sk2
                var x = document.getElementById("sk2");
                x.addEventListener("click", function() 
                {
                if(cnt>=pt2) {
                cnt=cnt-pt2;               
                var x = document.getElementById("sk2");
                x.disabled=true;
                var y = document.getElementById("sk3");
                y.disabled=false;

                var act = document.getElementById("sk2_act");
                act.disabled=false; 

                    
                var e0 = document.querySelector('#up2_3');
                e0.style.backgroundColor = "white";
	            }                      
                var divData=document.getElementById("ptNum");
                divData.innerText=cnt;
                } 
                );

                //sk3
                var x = document.getElementById("sk3");
                x.addEventListener("click", function() {
                if(cnt>=pt3) {
                cnt=cnt-pt3;
                var x = document.getElementById("sk3");
                x.disabled=true;
                var y = document.getElementById("sk4");
                y.disabled=false;

                var act = document.getElementById("sk3_act");
                act.disabled=false; 

                var e0 = document.querySelector('#up3_4');
                e0.style.backgroundColor = "white";
	            }                      
                var divData=document.getElementById("ptNum");
                divData.innerText=cnt;
                });

                //sk4
                var x = document.getElementById("sk4");
                x.addEventListener("click", function() {
                if(cnt>=pt4) {
                cnt=cnt-pt4;
                var x = document.getElementById("sk4");
                x.disabled=true;
                var act = document.getElementById("sk4_act");
                act.disabled=false;                 
	            }    
                var divData=document.getElementById("ptNum");
                divData.innerText=cnt;
                });

                //sk5
                var x = document.getElementById("sk5");
                x.addEventListener("click", function() {
                if(cnt>=pt5) {
                cnt=cnt-pt5;
                
                var x = document.getElementById("sk5");
                x.disabled=true; 
                var y = document.getElementById("sk6");
                y.disabled=false;

                var act = document.getElementById("sk5_act");
                act.disabled=false;  
                var e0 = document.querySelector('#up5_6');
                e0.style.backgroundColor = "white";
	            }                      
                var divData=document.getElementById("ptNum");
                divData.innerText=cnt;
                });

                //sk6
                var x = document.getElementById("sk6");
                x.addEventListener("click", function() {
                if(cnt>=pt6) {
                cnt=cnt-pt6;
                var x = document.getElementById("sk6");
                x.disabled=true;
                var y = document.getElementById("sk7");
                y.disabled = false;

                var act = document.getElementById("sk6_act");
                act.disabled=false;  
                var e0 = document.querySelector('#up6_7');
                e0.style.backgroundColor = "white";                      
	            }
                var divData=document.getElementById("ptNum");
                divData.innerText=cnt;
                });

                //sk7
                var x = document.getElementById("sk7");
                x.addEventListener("click", function() {
                if(cnt>=pt7) {
                cnt=cnt-pt7;
                var x = document.getElementById("sk7");
                x.disabled=true;
                var act = document.getElementById("sk7_act");
                act.disabled=false;                      
	            }  
                var divData=document.getElementById("ptNum");
                divData.innerText=cnt;
                
                });

                //sk8
                
                var x = document.getElementById("sk8");
                x.addEventListener("click", function() {
                if(cnt>=pt8) {
                cnt=cnt-pt8;
                var x = document.getElementById("sk8");
                x.disabled=true;
                var act = document.getElementById("sk8_act");
                act.disabled=false;                        
	            }
                var divData=document.getElementById("ptNum");
                divData.innerText=cnt;
                });      
