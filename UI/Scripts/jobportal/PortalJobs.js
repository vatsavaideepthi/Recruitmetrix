jQuery(document).ready(function () {


    //Code block to hide the bootstrap alert box 
    //following code hides the alertbox instead of removing it from the DOM
    //Code will be functional to the bootstrap alertboxes having 'alertclose' class only.

    $('.alertclose').click(function () {
        $(this).closest('.alert').addClass('hide');
    });
    
    //Code block to enable the Jquery UI Datetime picker for expirydate in Add Job Form
   
    $("#jobexpdate").datepicker({
        minDate: new Date(),
        onSelect: function (dateText, inst) {
            var date = $(this).val();
            $("#jobexpdateactual").val($(this).val());
            $('#jobexpdate').removeClass('alertred');
        }
    });
    //Code block to show the Jquery UI Datetime picker for expirydate in Add Job Form when it's trgger button clicked

    $('#jobexpdatetrigger').click(function () {
        $("#jobexpdate").datepicker("show");
    });
    //$('.updjobstatus').click(function () {
    //    $('#chosenapplicationid').val($(this).attr('data-jobid'));
    //    $('#UpdateJobStatus').modal('show');
    //});
  
    //To Activate or Deactivate Bulkmailing option for Jobs

    $('#requirementjobcollection .chkjob').change(function () {

        var checkedjobs = $('#requirementjobcollection .chkjob:checked');
        if (checkedjobs.length > 0) {
            $('#popbulkemail,.action-item').removeClass('disabled');
        }
        else {
            $('#popbulkemail,.action-item').addClass('disabled');
        }
    });

    //$('#jobapplicantcollection .chkcand').change(function () {

    //    var checkedcandidates = $('#jobapplicantcollection .chkcand:checked');
    //    if (checkedcandidates.length > 0) {
    //        $('#popbulkemail,.action-item').removeClass('disabled');
    //    }
    //    else {
    //        $('#popbulkemail,.action-item').addClass('disabled');
    //    }
    //});


    //Code to Genearate Bulk mail and activatting mail delivery system

    $('#popbulkemail').click(function (e) {
        var checkedjobs = $('#requirementjobcollection .chkjob:checked');
        $('#bulkjoblist').empty();
        var i = 0;
        $('#requirementjobcollection .chkjob:checked').each(function () {
            var jobblock = $(this).parents('.jobrow').clone();
            var urlpath = window.location.protocol + "//" + window.location.host + "/" + "jobs/post?jobid=" + $(jobblock).attr('data-jobid');
            var jobtitle = $(jobblock).find('.jobtitle');
            var joblocation = $(jobblock).find('.location');
            var jobexperience = $(jobblock).find('.experience');
            var jobskills = $(jobblock).find('.jobskills');
            if (i == 0) {
                var job = $('<li>').attr('style', 'list-style:none;padding:17px;border-top:1px solid #ccc;').append('<div><p style="margin: 0 0 3px;"><span style="font-size:15px;font-weight:bold;text-transform:capitalize;">' + jobtitle.text() + '</span > at<span style="text-transform:capitalize;"> ' + joblocation.text() + '</span></p>')
                            .append('<div><span >Skills : ' + jobskills.text() + '</span></div>')
                            .append('<div><span>Experience : ' + jobexperience.text() + '</span><a style="float:right;" href="' + urlpath + '"' + '>Reply Job</a></div>')
                            .append('</div></li>');

                $('#bulkjoblist').append(job);
            }
            else {
                var job = $('<li>').attr('style', 'list-style:none;padding:8px;').append('<div><p><span style="font-size:18px;text-transform:capitalize;">' + jobtitle.text() + '</span > at<span> ' + joblocation.text() + '</span></p>')
                            .append('<div><span>Skills : ' + jobskills.text() + '</span></div>')
                            .append('<div><span>Experience : ' + jobexperience.text() + '</span><a style="float:right;" target="_blank" href="' + urlpath + '"' + '>Reply Job</a></div>')
                            .append('</div></li>');
                //href="http:\/\/localhost:4847/jobs/post?jobid=' + $(jobblock).attr('data-jobid') + '\"' + '
                $('#bulkjoblist').append(job);
            }

            var loopjobid = $(jobblock).attr('data-jobid');
            if ($('#jobcollectionstring').val() == "") {
                $('#jobcollectionstring').val(loopjobid);
            }
            else {
                $('#jobcollectionstring').val($('#jobcollectionstring').val() + ';' + loopjobid)
            }
        })
        if ($('#requirementjobcollection .chkjob:checked').length > 0) {
            $('#SendJobBulkMail').modal('show');
        }
        $('#mailmessagesubject').val('Company Has Shared Jobs With You');
        var mailcontent = $('#mailbodyholder').clone();
        $(mailcontent).find('.emailpreviewline').remove();
        $('#mailmessagebody').val($(mailcontent).html());
    });

    //Code Block to display the modal popup window which facilitates the options to update the job status 

    $('.updjobstatus').click(function () {
        $('#chosenqjobid').val($(this).attr('data-jobid'));
        $('#UpdateJobStatus').modal('show');
    });

    $('.delete-job').click(function () {
        $('#chosenjobid').val($(this).attr('data-jobid'));
        $('#jobtitle').text($(this).attr('data-title'));
        $('.delete-job').parents().closest('.jobrow').addClass('abouttodelete');
        $('#deletejob').modal('show');
    });

    //Code block to reset job status update form on Job update status Modal popup window closing
    $('#UpdateJobStatus').on('hide', function () {
        $('#resetupdjobstatusform').click();
    });

    //Code Block to the job status
    //currentjobid -- The uniqueid of the job which user selected to update the status
    //modifiedstatusid -- new changed status of the job which user selected for the job
    //Page will be refreshed once the following function executes
    $('#updatecurrentjobstatus').click(function () {

        var currentjobid = $('#chosenqjobid').val();
        var modifiedstatusid = $('#modal-upd-jobstatus option:selected').index();

        $.ajax({
            url: "/Jobs/UpdateJobStatus",
            type: "POST",
            dataType: "JSON",
            data: {
                jobid: currentjobid,
                status: modifiedstatusid
            },
            async: false,
            success: function (data) {
                $('#UpdateJobStatus').modal('hide');
                $('#jobstatuschangesuccess').removeClass('hide');
                $('#jobstatuschangesuccess').delay(3000).fadeOut().addClass('hide');
            },
            error: function (data) {
            }
        });
        $('#UpdateJobStatus').modal('hide');
        location.reload();
    });

    $('#deletejobyes').click(function () {

        var currentjobid = $('#chosenjobid').val();
        $.ajax({
            url: "/Jobs/Deletejob",
            type: "POST",
            dataType: "JSON",
            data: {
                jobid: currentjobid,
                categoryid: '',
                subcategoryid:'',
                
            },
            async: false,
            success: function (data) {
                $('#deletejob').modal('hide');
                $('.abouttodelete').hide();

            },
            error: function (data) {
            }
        });
        $('#deletejob').modal('hide');
        location.reload();
    });

    $('#deletejobno').click(function () {
        $('#deletejob').modal('hide');
    });


    //#region Validation Code Blocks

    //Code Block to validate the field values in add job form 
    //User will be notified with alerts if add job ruleset defined in the function not validated successfully
    //Add Job form will be submitted once the entries will meet the add job rule set criteria

    $('#btnpostforValidation').click(function () {

        var validcode = '1111111111';
        var testcode = '';

        //Job title Validation Starts
        if ($('#jobtitle').val().trim() == "") {
            $('#jobtitle').addClass('alertred');
            $('#jobtitle').popover('show');
            testcode += '0';
        }
        else {
            //$('#jobtitleempty.alert').addClass('hide');
            $('#jobtitle').removeClass('alertred');
            testcode += '1';
        }
        //Job title Validation Ends

        //Job Location Validation Starts
        if ($('#joblocation').val().trim() == "") {
            $('#joblocation').addClass('alertred');
            $('#joblocation').popover(
                {
                    placement:'top',
                    content:'Location Cannot Be Empty'
                }
                ).popover('show');
            testcode += '0';
        }
        else {
            $('#jobtitle').removeClass('alertred');
            testcode += '1';
        }
        //Job Location Validation Ends

        //Job Skills Validation Starts
        if ($('#jobskills').val().trim() == "") {
            $('#jobskills').addClass('alertred');
            $('#jobskills').popover(
                {
                    placement: 'right',
                    content: 'Skills Cannot Be Empty'
                }
                ).popover('show');
            testcode += '0';
        }
        else {
            //$('#jobskillsempty.alert').addClass('hide');
            $('#jobskillsempty').removeClass('alertred');
            testcode += '1';
        }
        //Job Skills Validation ends

        //Job Postion type Validation Starts
        //if ($('#postype :selected').index() == 0) {
        //    $('#jobpositionconainer').addClass('alertred');
        //    //$('#postype').popover(
        //    //    {
        //    //        placement:'top',
        //    //        content: 'Select the position type'
        //    //    }
        //    //    ).popover('show');
        //    testcode += '0';
        //}
        //else {
        //    //$('#jobpositionempty.alert').addClass('hide');
        //    $('#jobpositionconainer').removeClass('alertred');
        //    testcode += '1';
        //}

        if ($(".chosen-select-pos").val() == null) {
                $('#jobpositionconainer').addClass('alertred');
                //$('#postype').popover(
                //    {
                //        placement:'top',
                //        content: 'Select the position type'
                //    }
                //    ).popover('show');
                testcode += '0';
            }
            else {
                //$('#jobpositionempty.alert').addClass('hide');
                $('#jobpositionconainer').removeClass('alertred');
                testcode += '1';
            }


        //Job Postion type Validation Ends

        //Job Experience Validation Starts
        if ($('#Exp :selected').index() == 0) {
            $('#jobexpcontainer').addClass('alertred');
            //$('#Exp').popover(
            //    {
            //       placement:'top',
            //        content: 'Select the Experience'
            //    }
            //   ).popover('show');
            testcode += '0';
        }
        else {
            //$('#jobexperienceempty.alert').addClass('hide');
            $('#jobexpcontainer').removeClass('alertred');
            testcode += '1';
        }
        //Job Experience Validation ends

        //Job Payrate Validation Starts
        if ($('#jobsalary').val() == "") {
            $('#jobsalary').addClass('alertred');
            $('#jobsalary').popover(
                {
                    content: 'Provide the Payrate',
                    placement:'left'
                }
               ).popover('show');
            testcode += '0';
        }
        else {
            //$('#jobsalaryempty.alert').addClass('hide');
            $('#jobsalary').removeClass('alertred');
            testcode += '1';
        }
        //Job Payrate Validation Ends

        //Job Expirydate Validation Starts
        if ($('#jobexpdate').val() == "") {
            $('#jobexpdate').addClass('alertred');
            $('#jobexpdatetrigger').popover(
                {
                    content: 'Select the Job Expiry Date',
                    placement:'bottom'
                }
               ).popover('show');
            testcode += '0';
        }
        else {
            //$('#jobexpdateempty.alert').addClass('hide');
            $('#jobexpdateempty').removeClass('alertred');
            testcode += '1';
        }
        //Job Expirydate Validation Ends

        //Job Description Validation Starts
        if ($('#JobDesc').val().trim() == "") {
            $('#JobDesc').addClass('alertred');
            $('#JobDesc').popover(
                {
                    content: 'Job description cannot be Empty',
                    plcaement:'right'
                }
                ).popover('show');
            testcode += '0';
        }
        else {
            //$('#jobdescempty.alert').addClass('hide');
            $('#jobdescempty').removeClass('alertred');
            testcode += '1';
        }
        //Job Expirydate Validation Ends

        //Poster email validation intiates

        if ($('#jobposteremail').val().trim() == "") {
            $('#jobposteremail').addClass('alertred');

            testcode += '0';
        }
        else {
            if (ValidateEmail($('#jobposteremail').val())) {
                $('#jobposteremail').removeClass('alertred');
                //alert('success valid Email');
                testcode += '1';
            }
            else {
                $('#jobposteremail').addClass('alertred');
                //alert('In Valid Email');
                testcode += '0';
            }

        }

        //Poster email validation ends here

        //Poster Contact validation initiated

        if ($('#jobpostercontact').val().trim() == "") {
            $('#jobpostercontact').addClass('alertred');
            testcode += '0';
        }
        else {
            //$('#jobtitleempty.alert').addClass('hide');
            $('#jobpostercontact').removeClass('alertred');
            testcode += '1';
        }

        //Poster Contact validation ends here

        if (testcode == validcode) {
            $('#addjobform').submit();
        }
        else {
            //alert('User Attention Required');
        }
    });

    $('#jobpostercontact').on('input propertychange', function () {
        if ($('#jobpostercontact').val().trim() != "" && $('#jobpostercontact').val().trim().replace(/[ ]/g, "").length < 10) {
            $('#jobpostercontact').removeClass('alertred');
        }
        else {
            $('#jobpostercontact').addClass('alertred');
        }
    });
    
    $('#jobposteremail').on('input propertychange', function () {
        if ($('#jobposteremail').val().trim() == "") {
            $('#jobposteremail').removeClass('alertred');
        }
        else {
            $('#jobposteremail').addClass('alertred');
            if (ValidateEmail($('#jobposteremail').val())) {
                $('#jobposteremail').removeClass('alertred');
            }
            else {
                $('#jobposteremail').addClass('alertred');
                //alert('In Valid Email');
            }

        }
    });

    //#endregion


    //Code Blocks to destroy the popovers when data entered into the fields

    $('#jobtitle').on('input', function(){
    
        if ($('#jobtitle').val().length > 0)
        {
            $('#jobtitle').popover('destroy');
            $('#jobtitle').removeClass('alertred');
        }
    });

    $('#joblocation').on('input', function () {

        if ($('#joblocation').val().length > 0) {
            $('#joblocation').popover('destroy');
            $('#joblocation').removeClass('alertred');
        }
    });

    $('#jobsalary').on('input', function () {

        if ($('#jobsalary').val().length > 0) {
            $('#jobsalary').popover('destroy');
            $('#jobsalary').removeClass('alertred');
        }
    });

    $('#jobexpdate').on('input', function () {

        if ($('#jobexpdate').val().length > 0) {
            $('#jobexpdate').popover('destroy');
            $('#jobexpdate').removeClass('alertred');
        }
    });

    $('#jobskills').on('input', function () {

        if ($('#jobskills').val().length > 0) {
            $('#jobskills').popover('destroy');
            $('#jobskills').removeClass('alertred');
        }
    });

    $('#JobDesc').on('input', function () {

        if ($('#JobDesc').val().length > 0) {
            $('#JobDesc').popover('destroy');
            $('#JobDesc').removeClass('alertred');
        }
    });

    $('#addjobform').on('change', '#postype', function () {
        if ($('#postype :selected').index() != 0)
        {
            $('#jobpositionconainer').removeClass('alertred');
        }
    })

    $('#addjobform').on('change', '#Exp', function () {
        if ($('#Exp :selected').index() != 0) {
            $('#jobexpcontainer').removeClass('alertred');
        }
    })

    $('#updatejobinformationproxy').click(function () {
        $('#currentjobidedit').val($(this).attr('data-jobid'));
        $('#editjobform').submit().first();
    });

    $('.getjobnotes').click(function () {

        $('#currentjobid').val($(this).attr('data-jobid'));

        $.ajax({
            url: "/Jobs/GetNotes",
            type: "POST",
            dataType: "JSON",
            data: {
                
                parentid: '',
                applicationid: $('#currentjobid').val(),
                objecttype: "notes"
            },
            async: false,
            success: function (data) {
                $('#jobnotescontainer').modal('show');
                if (data.length > 0) {
                    $('#jobnotescontainer #emptyjobnotesholder').addClass('hide');
                    $('#jobnotescontainer #jobnoteslistholder').removeClass('hide');
                    var result = data;
                    notesforjob(result);
                }
                else {
                    $('#jobnotescontainer #emptyjobnotesholder').removeClass('hide');
                    $('#jobnotescontainer #jobnoteslistholder').addClass('hide');
                }

            },
            error: function (data) {
            }
        });


    });

    function notesforjob(finalresult) {
        $('.jobnotes').empty();
        $(finalresult).each(function () {

            var userdisplaytitle = "";
            if (this.modifiedby.FirstName.trim() == "") {
                if (this.modifiedby.LastName.trim() == "") {
                    userdisplaytitle = this.modifiedby.Email.trim();
                }
                else {
                    userdisplaytitle = this.modifiedby.LastName.trim();
                }
            }
            else {
                userdisplaytitle = this.modifiedby.FirstName.trim();
            }
            var notes = $('<li class="notesrow" >').attr('style', 'list-style:none;padding:10px 0px;')
                        .append('<div><label class="notesdescription"> ' + this.des.trim() + '</label>')
                        .append('<div><ul class="notes-meta-data pull-left" style=list-style:none;><li>Updated by <a>' + userdisplaytitle + '</a>&nbsp' + this.modifieddate + '</li></ul></div>')
                        .append('<ul class="notes-actions-set pull-right" style=list-style:none;><li ><a class="notesbtupdatejob" data-noteid=' + this.id + '><i class="glyphicon glyphicon-edit"></i></a></li><li><a class="notes-bt-delete hide "><i class="icon-trash"></i></a></li></ul>')
                        .append('</li>');
            $('.jobnotes').append(notes)
        });
    }

    $('#jobnotescontainer').on('hide', function () {
        $('#resetnotesjobform').click();
    }

  );
    $('#btnaddnewnotesjob').click(function () {

        $('.top-action-set').addClass('hide');
        $('#jobnotesmodelheader').text('Add New Notes');
        $("#jobnoteslistholder").addClass('hide');
        $('#emptyjobnotesholder').addClass('hide')
        $('#back').addClass('hide');
        $('#jobnewnotesholder').removeClass('hide');
        $('#jobnotesactioncontainer').removeClass('hide');
    });
    $('#notesmodelheader').text('Notes');


    $('#jobnotesactioncontainer #jobcancelnotes').click(function () {
        $('#jobnotesmodelheader').text('Notes');
        $('#btnaddnewnotes').removeClass('hide');
        if ($('#jobnoteslistholder .jobnotes').find('li').length > 0) {
            $("#jobnoteslistholder").removeClass('hide');
            $('#emptyjobnotesholder').addClass('hide');
        }
        else {
            $("#jobnoteslistholder").addClass('hide');
            $('#emptyjobnotesholder').removeClass('hide');
        }
        $('#jobnewnotesholder').addClass('hide');
        $('#back').removeClass('hide');
        $('#jobnotesactioncontainer').addClass('hide');
        $('.top-action-set').removeClass('hide');
    });

    $('#jobnotesactioncontainer #jobsavenewnotes').click(function () {
        var testcode = '';
        if ($('#jobnewnotescontent').val().trim() == "") {
            $('#attachcandalert').addClass('hide');
            $('#jobnewnotescontent').addClass('alertred');
            $('#jobnewnotescontent').css("cssText", "border:1px solid red !important;width: 98%; padding: 4px 0px 7px 10px !important;");
            testcode += '0';
        }
        else {
            $('#attachcandalert').addClass('hide');
            $('#jobnewnotescontent').removeClass('alertred');
            $('#jobnewnotescontent').css("cssText", "width: 98%; padding: 4px 0px 7px 10px !important;");
            testcode += '1';
            $.ajax({
                url: "/Jobs/AddNotes",
                type: "POST",
                dataType: "JSON",
                data: {
                    applicationid: $('#currentjobid').val(),
                    subjecttype: "job",
                    description: $("#jobnewnotescontent ").val()
                },
                async: false,
                success: function (data) {
                    $('.top-action-set').addClass('hide');
                    $('#jobnotescontainer').modal('hide');
                },
                error: function (data) {
                }
             
            });
            $('#jobnotescontainer').modal('hide');
            location.reload();
        }
    });

    $('.jobnotes').on('click', '.notesbtupdatejob', function () {
        $('#jobnewnotesholder').removeClass('hide');
        $('#emptyjobnotesholder').addClass('hide');
        $('#jobnoteslistholder').addClass('hide');     
        $(this).data("noteid");
        $('#jobnewnotescontent').val($(this).closest('.notesrow').find('.notesdescription').text());
        $('#updjobnotes').removeClass('hide');
        $('#btnaddnewnotesjob').addClass('forcehide');
        $('#back').addClass('hide');
        $('#updatebuttonjobnotes').attr('data-noteid', $(this).data("noteid"));
    });

    $('#updatebuttonjobnotes').click(function () {
        $.ajax({
            url: "/Jobs/UpdateNotes",
            type: "POST",
            dataType: "JSON",
            data: {
                commentid: parseInt($(this).data("noteid")),
                subjectid: $('#currentjobid').val(),
                subjecttype: 'job',
                objecttype: 'notes',
                Des: $("#jobnewnotescontent").val(),
                visibility: 1,
            },
            async: false,
            success: function (data) {
                $('#jobnotescontainer').modal('hide');
                $('#btnaddnewnotesjob').removeClass('forcehide');
            },
            error: function (data) {
            }
        });
        $('#jobnotescontainer').modal('hide');
        location.reload();
      
        
    });

    $('#cancelnotesupdate').click(function () {
        $('#jobnewnotesholder').addClass('hide');
        $('#jobnoteslistholder').removeClass('hide');
        $('#jobnewnotescontent').val("");
        $('#updjobnotes').addClass('hide');
        $('#btnaddnewnotesjob').removeClass('forcehide');
        $('#back').removeClass('hide');
        $('#updatebuttonjobnotes').attr('data-noteid', "");
    });
      
    $('#deleteapplicantyes').click(function () {

        var currentapplicantid = $('#chosenapplicantid').val();
        $.ajax({
            url: "/Jobs/Deleteapplicant",
            type: "POST",
            dataType: "JSON",
            data: {
                transactionid: currentapplicantid,
            },
            async: false,
            success: function (data) {
                $('#deleteapplicant').modal('hide');
                $('.abouttodelete').hide();

            },
            error: function (data) {
            }
        });
        $('#deleteapplicant').modal('hide');
     });

    $('#deleteapplicantno').click(function () {
        $('#deleteapplicant').modal('hide');
    });

    $('.add-candidate').click(function () {
        $('#chosenappjobid').val($(this).attr('data-jobid'));
       
        $('#candidatesaddtoapplicant').modal('show');
        $.ajax({
            url: "/Candidate/Getapplicants",
            type: "POST",
            dataType: "JSON",
            data: {
                jobid: $('#chosenappjobid').val(),
               
            },
            async: false,
            success: function (data) {
                $('#candidatesaddtoapplicant').modal('show');
                if (data.length > 0) {
                    var result = data;
                    candidatesinjob(result);
                }
                else {
                   
                }

            },
            error: function (data) {
            }
        });

    });

    function candidatesinjob(finalresult) {
        $('.getcandidates').empty();

        $(finalresult).each(function () {
           
            var candidates = $('<li class="row">').attr('style', 'list-style:none;padding:10px 0px;')
                        .append('<div id="candidates1" data-id="'+this.candidateid+'"><input type="checkbox" class="chkchk"><span id="firstname"style="color: #16a085;font-weight: bold;text-transform: capitalize;font-size: 15px;text-decoration: none;">'+ jQuery.parseJSON(this.data).firstname+'</span><span id="candidateemail" data-candidateid='+this.candidateid+'style="margin-left:17px;margin-top:10px;"> Email:' + jQuery.parseJSON(this.data).person.email + '</span><span id="detailedinfolocation" style="margin-left:17px;margin-top:10px;">Location:' + jQuery.parseJSON(this.data).detailedinfo.location + '</span><span id="candidateskills" style="margin-left:17px;margin-top:10px;">Skills:' + jQuery.parseJSON(this.data).job.skills.primary + '</span>')
                        .append('</li>');
            $('.getcandidates').append(candidates)
        });
    }

    $('#addcandidates').click(function (e) {
        var checkedjobs = $('#candidates1 .chkchk:checked');
     
        $('#candidates1 .chkchk:checked').each(function () {
            var candidatesblock = $(this).parents().closest('.row');

            var firstname = $(this).parents().closest('.row').find('#firstname');
            var email = $(this).parents().closest('.row').find('#candidateemail');
            var skills = $(this).parents().closest('.row').find('#candidateskills');
            var location = $(this).parents().closest('.row').find('#detailedinfolocation');
            var candidid = $(this).parents().closest('.row').find('#candidates1').attr('data-id');
         
            $.ajax({
                url: "/Candidate/Addcandidates",
                type: "POST",
                dataType: "JSON",
                data: {
                    jobid: $('#chosenappjobid').val(),
                    candidateid: candidid
                    },
                async: false,
                success: function (data) {
                    $('#candidatesaddtoapplicant').modal('hide');

                    // $('.abouttodelete').hide();

                },
                error: function (data) {
                }
            });
    

        });

        $('#candidatesaddtoapplicant').modal('hide');

        location.reload();
    });

  });

