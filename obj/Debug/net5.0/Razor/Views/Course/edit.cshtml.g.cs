#pragma checksum "/Users/mac/Desktop/naturepower/Views/Course/edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8322cdd2627eb579ec01e9b1adcbb255eb90eb0d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Course_edit), @"mvc.1.0.view", @"/Views/Course/edit.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/mac/Desktop/naturepower/Views/_ViewImports.cshtml"
using nature;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/mac/Desktop/naturepower/Views/_ViewImports.cshtml"
using nature.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8322cdd2627eb579ec01e9b1adcbb255eb90eb0d", @"/Views/Course/edit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bc1f54a396025cac51c4e4562ae65e801a2b3cbe", @"/Views/_ViewImports.cshtml")]
    public class Views_Course_edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<nav aria-label='breadcrumb'>
    <ol class='breadcrumb breadcrumb-arrow'>
        <li class='breadcrumb-item'><a href='/course/index'>course</a></li>
        <li class='breadcrumb-item active' aria-current='page'>edit</li>
    </ol>
</nav>
<div id='app1' v-cloak>

    <v-app>
         
        <v-card>
          <v-card-title>
          </v-card-title>
          <v-card-text>
            <div style=""margin:20px""><v-text-field  v-model=""course.courseName"" label=""course"" ></v-text-field></div>

<div style=""margin:20px""><v-text-field  v-model=""course.fee"" label=""fee"" ></v-text-field></div>

<div style=""margin:20px""><v-text-field  v-model=""course.discount"" label=""discount"" ></v-text-field></div>

 <v-date-picker
      v-model=""course.createdDate""
      class=""mt-4""
      
      
></v-date-picker>


          </v-card-text>
          <v-card-actions>
             <v-btn ");
            WriteLiteral("@click=\'edit_course\' color=\'blue\' outlined>\n                <v-icon>\n                    mdi-content-save-edit\n                </v-icon>\n             </v-btn>\n          </v-card-actions>\n          \n        </v-card>\n    </v-app>\n</div>\n");
            DefineSection("scripts", async() => {
                WriteLiteral(@"
    <script>
        var app;
            var component = {
                vuetify: new Vuetify(theme())
                ,
                el:'#app1'
                ,
                data:{
                    course:{}
                    ,
                    
                }//edata
                ,
                created(){
                  this.course = ");
#nullable restore
#line 56 "/Users/mac/Desktop/naturepower/Views/Course/edit.cshtml"
                           Write(Html.Raw(Json.Serialize(@ViewBag.course)));

#line default
#line hidden
#nullable disable
                WriteLiteral(@";
                     
                     
                }//ecreated
                ,
                methods:{
                  edit_course(){
                   
                    var url = '/course/edit';
                    var param= this.course;
                    $.post(url,param)
                    .done(res =>{
                          if(res.error == -1){
                               window.location = '/course/index';
                          }
                          else{
                             alert(res.exception);
                          }
                    });
                    
                  }//ef
                  ,                 
                  

                }//emethod
                ,
                 computed:{

                 }//ecomputed
            };
            app = new Vue(component);


    </script>

");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
