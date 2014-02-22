
jQuery(document).ready(function () {

    $('.onumeric').numeric();

    function applicant(id, fname, mname, lname, person, address, detailedinfo, job, supplier, resume) {
        this.id = id;
        this.firstname = fname;
        this.middlename = mname;
        this.lastname = lname;
        this.person = person;
        this.address = address;
        this.detailedinfo = detailedinfo;
        this.job = job;
        this.supplier = supplier;
        this.resume = resume;

    }

    //#region Person

    function person(name, email, compname, contact) {
        this.name = name;
        this.email = email;
        this.compname = compname;
        this.contact = contact;
    }

    //#endregion

    //#region Address

    function address(line1, line2, city, cityid, state, country, pincode) {
        this.line1 = line1;
        this.line2 = line2;
        this.city = city;
        this.cityid = cityid;
        this.state = state;
        this.country = country;
    }

    //#endregion

    //#region JOB

    function job(experience, category, subcategory, skills) {
        this.experience = experience;
        this.category = category;
        this.subcategory = subcategory;
        this.skills = skills;
    }

    function category(id, name) {
        this.id = id;
        this.name = name;
    }

    function subcategory(id, name) {
        this.id = id;
        this.name = name;
    }

    function skills(primary, additional) {
        this.primary = primary;
        this.additional = additional;
    }

    //#endregion

    //#region Supplier
    function supplier(company, person, address) {
        this.company = company;
        this.person = person;
        this.address = address;
    }

    //#endregion

    function resume(id, url) {
        this.id = id;
        this.url = url;
    }


    //#region Detailed Info

    function detailedinfo(education, visa, payrate, salary, availability, notes, relocation,value, location, travel, addinfo) {
        this.education = education;
        this.visa = visa;
        this.payrate = payrate;
        this.salary = salary;
        this.availability = availability;
        this.notes = notes;
        this.relocation = $('#jobcandreloaction :selected').text();
        this.value = $('#txtYes').val();
        this.location = location;
        this.travel = travel;
        this.addinfo = addinfo;
        this.appliedby = $('input[name=optionsRadios]:checked').val();
    }

    function education(qualification, degree) {
        this.qualification = qualification;
        this.degree = degree;
    }

    function qualification(primary, other) {
        this.primary = primary;
        this.other = other;
    }

    function degree(title, usdegree) {
        this.title = title;
        this.usdegree = usdegree;
    }

    function notes(id, data) {
        this.id = id;
        this.data = data;
    }

    //#endregion
    $('.addapplicant').click(function (e) {
                
        var validcode = '1111111111';
        var testcode = '';
        if ($('#candname').val().trim() == "") {
            $('#candname').addClass('alertred');
            testcode += '0';
        }
        else {
         
            $('#candname').removeClass('alertred');
            testcode += '1';
        }

        if ($('#candskills').val().trim() == "") {
            $('#candskills').addClass('alertred');
            testcode += '0';
        }
        else {
            $('#candskills').removeClass('alertred');
            testcode += '1';
        }
        if ($('#candlocation').val().trim() == "") {
            $('#candlocation').addClass('alertred');
            testcode += '0';
        }
        else {
            $('#candlocation').removeClass('alertred');
            testcode += '1';
        }

        if ($('#candexperiance').val().trim() == "") {
            $('#candexperiance').addClass('alertred');
            testcode += '0';
        }
        else {
            $('#candexperiance').removeClass('alertred');
            testcode += '1';
        }


        if ($('#candpayrate').val().trim() == "") {
            $('#candpayrate').addClass('alertred');
            testcode += '0';
        }
        else {
              
            $('#candpayrate').removeClass('alertred');
            testcode += '1';
        }

        if ($('#suppliercontactemail').val().trim() == "") {
                
            $('#suppliercontactemail').addClass('alertred');
            testcode += '0';
        }
        else {
               
               
            if (ValidateEmail($('#suppliercontactemail').val())) {
                $('#suppliercontactemail').removeClass('alertred');
                //alert('success valid Email');
                testcode += '1';
            }
            else {
                $('#suppliercontactemail').addClass('alertred');
                //alert('In Valid Email');
                testcode += '0';
            }
        }

        if ($('#suppliercontactnumber').val().trim() == "") {
              
            $('#suppliercontactnumber').addClass('alertred');
            testcode += '0';
        }
        else {
               
            $('#suppliercontactnumber').removeClass('alertred');
            testcode += '1';
        }

        if ($('#candwrkauthlist option:selected').text() == 'Work Authorization') {
            
            $('#candwrkauthlist').addClass('alertred');
            testcode += '0';
        }
        else {
              
            $('#candwrkauthlist').removeClass('alertred');
            testcode += '1';
        }

        if ($('#jobcandreloaction option:selected').text() == 'Relocation') {
               
            $('#jobcandreloaction').addClass('alertred');
            testcode += '0';
        }
        else{
              
            $('#jobcandreloaction').removeClass('alertred');
            testcode += '1';
                 
    }

        if ($('#candresume').prop("files")[0] != undefined) {
               
            $('#browseapplicantresume').removeClass('alertred');
            testcode += '1';
        }
        else {
               
            $('#browseapplicantresume').addClass('alertred');
            testcode += '0';
        }

        if (testcode == validcode) {
               
            ///Prepare Objects

            suppliercontact = $('#suppliercontactnumber').val();

            var supplierperson = new person(
                                             $('#suppliername').val(),
                                             $('#suppliercontactemail').val(),
                                             $('#suppliercompname').val(),
                                                suppliercontact
                                            );
            var supplieraddress = new address('', '', '', '', '', '', '');

            var supplierdata = new supplier($('#suppliercompname').val(), supplierperson, supplieraddress);

            var jobskills = new skills($('#candskills').val(), '');
            var jobcategory = new category('', '');
            var jobsubcategory = new subcategory('', '')
            var jobdata = new job($('#candexperiance').val(), jobcategory, jobsubcategory, jobskills);


            var dinfoqualification = new qualification('', '');
            var dinfodegree = new degree('', '');

            var dinfoeducation = new education(dinfoqualification, dinfodegree);

            var dinfonotes = new notes('', '');

            var workauthselection = $("#candwrkauthlist option:selected").text();
            if (workauthselection == "Work Authorization") {
                workauthselection = "";
            }
            var detailedinfodata = new detailedinfo(dinfoeducation, workauthselection, $('#candpayrate').val(), '', '', dinfonotes, $("#jobcandreloaction option:selected").text(),$("#txtYes").val(), $('#candlocation').val(), '', '');

            var applicantdata = new applicant($('#candgenid').val(), $('#candname').val(), '', '', supplierperson, supplieraddress, detailedinfodata, jobdata, supplierdata, null);
              
            $('#applicationdata').val(JSON.stringify(applicantdata));

            $('.addapplicant').hide();
            $('#addapplicantdummy').show();
            $('#addcandidateform').submit();
               
            //End Of Preparing Objects
        }
        else {
            // $('#attachcandalert').show();
        }      
    });

    $('#candname').on('input', function () {

        if ($('#candname').val().length > 0) {
            $('#candname').removeClass('alertred');
        }
    });

    $('#candskills').on('input', function () {

        if ($('#candskills').val().length > 0) {
            $('#candskills').removeClass('alertred');
        }
    });

    $('#candlocation').on('input', function () {

        if ($('#candlocation').val().length > 0) {
            $('#candlocation').removeClass('alertred');
        }
    });

    $('#candexperiance').on('input', function () {

        if ($('#candexperiance').val().length > 0) {
            $('#candexperiance').removeClass('alertred');
        }
    });

    $('#candpayrate').on('input', function () {

        if ($('#candpayrate').val().length > 0) {
            $('#candpayrate').removeClass('alertred');
        }
    });

    $('#suppliercontactemail').on('input', function () {

        if ($('#suppliercontactemail').val().length > 0) {
            $('#suppliercontactemail').removeClass('alertred');
        }
    });

    $('#suppliercontactnumber').on('input', function () {

        if ($('#suppliercontactnumber').val().length > 0) {
            $('#suppliercontactnumber').removeClass('alertred');
        }
    });



});



jQuery(document).ready(function () {

    $("#emailtocandidatebody").jqte();
    $("#emailcandidate").jqte();
    $('#candexperiance').decimal();

    $('#candpayrate').numeric();            

    $('.updapplicantstatus').click(function () {
        $('#chosenapplicationid').val($(this).parents().closest('.jobrow').attr('data-applicationid'));
        $('#chosencandidateid').val($(this).parents().closest('.jobrow').attr('data-candidateid'));
     
        $('#UpdateApplicantStatus').modal('show');
    });

    $('#UpdateApplicantStatus').on('hide', function () {
        $('#resetupdapplicationstatusform').click();
    });

    $('#updateApplicant').click(function () {

        $.ajax({
            url: "/Jobs/UpdateApplicantStatus",
            type: "POST",
            dataType: "JSON",
            data: {
                applicationid: $('#chosenapplicationid').val(),
                candidateid: $('#chosencandidateid').val(),
                status: $("#applicationstatus option:selected").text()
            },
            async: false,
            success: function (data) {
               
                    $('#UpdateApplicantStatus').modal('hide');           

            },
            error: function (data) {
           
            }

        });
        $('#UpdateApplicantStatus').modal('hide');
        location.reload();
    });
    
    $('.updapplicantquestionaire').click(function () {
        var questionaireupdateelem = this;
        $('#chosenqapplicationid').val($(this).parents().closest('.jobrow').attr('data-applicationid'));
        $('#chosenqcandidateid').val($(this).parents().closest('.jobrow').attr('data-candidateid'));
        $('#chosenapplicationdata').val($(this).parents().closest('.jobrow').attr('data-applicantdata'));

        $('.modal-upd-payrate').val($(this).parents().closest('.jobrow').find('.crpayrate').attr('data-payrate'));
        $('#modal-upd-wrkauth').val($(this).parents().closest('.jobrow').find('.crvisa').attr('data-visa'));
        $('.modal-upd-location').val($(this).parents().closest('.jobrow').find('.crlocation').attr('data-location'));
        var relocationoption = $(this).parents().closest('.jobrow').find('.crrelocation').text().trim().toUpperCase();
        if (relocationoption === "YES" || relocationoption === "NO")
        {
           
        }
         
        else
        {
           
        }

        switch (relocationoption)
        {
            case "YES":
                $("#modal-upd-relocation").val("2");
                var chosenlocationforrelocation = $(this).parents().closest('.jobrow').find('.relocation-locations-value').text();
                $('.modal-upd-relocation-locations').val(chosenlocationforrelocation);
                // $('.chosenlocations').hide();
                $('.chosenlocations').show();
                break;
            case "NO":
                $("#modal-upd-relocation").val("1");
                $('.chosenlocations').hide();
                break;
            case "YES TO SPECIFIC":
                var chosenlocationforrelocation = $(this).parents().closest('.jobrow').find('.relocation-locations-value').text();
                $("#modal-upd-relocation").val("3");
                $('.modal-upd-relocation-locations').val(chosenlocationforrelocation);
                $('.chosenlocations').show();
                break;
        }
        $('#UpdateApplicantQuestionaire').modal('show');
    });

    $('#UpdateApplicantQuestionaire').on('hide', function () {
        $('#resetupdquestionaireform').click();
    });

    $('#updateApplicantquestionaire').click(function () {

        var applicantdata = jQuery.parseJSON($('#chosenapplicationdata').val());
        applicantdata.detailedinfo.relocation = $("#modal-upd-relocation option:selected").text()
        applicantdata.detailedinfo.value = $(".modal-upd-relocation-locations").val()
        applicantdata.detailedinfo.location = $('.modal-upd-location').val();
        applicantdata.detailedinfo.visa = $('#modal-upd-wrkauth option:selected').text();
        applicantdata.detailedinfo.payrate = $('.modal-upd-payrate').val();
        
        $.ajax({
            url: "/Jobs/UpdateQuestionaire",
            type: "POST",
            dataType: "JSON",
            data: {
                applicationid: $('#chosenqapplicationid').val(),
                data: JSON.stringify(applicantdata)
            },
            async: false,
            success: function (data) {
                $('#UpdateApplicantQuestionaire').modal('hide');
            },
            error: function (data) {
            }
        });
        $('#UpdateApplicantQuestionaire').modal('hide');
        location.reload();
     
    });

    $('.getappnotes').click(function () {
        
        $('#currentapplicationid').val($(this).attr('data-applicationid'));
       
        $.ajax({
            url: "/Jobs/GetNotes",
            type: "POST",
            dataType: "JSON",
            data: {
                applicationid: $('#currentapplicationid').val(),
                parentid:'',
                objecttype:"notes"
            },
            async: false,
            success: function (data) {
                $('#applicantnotescontainer').modal('show');
                if (data.length > 0) {
                    $('#applicantnotescontainer #emptynotesholder').addClass('hide');
                    $('#applicantnotescontainer #noteslistholder').removeClass('hide');
                    var result = data;
                    appnotesinjob(result);
                }
                else
                {
                    $('#applicantnotescontainer #emptynotesholder').removeClass('hide');
                    $('#applicantnotescontainer #noteslistholder').addClass('hide');
                }
    
            },
            error: function (data) {
            }
        });
       

    });

    function appnotesinjob(finalresult) {
        $('.applicantnotes').empty();
        
        $(finalresult).each(function () {
            var userdisplaytitle = "";
            if (this.modifiedby.FirstName.trim()=="")
            {
                if (this.modifiedby.LastName.trim() == "") {
                    userdisplaytitle = this.modifiedby.Email.trim();
                }
                else
                {
                    userdisplaytitle = this.modifiedby.LastName.trim();
                }
            }
            else
            {
                userdisplaytitle = this.modifiedby.FirstName.trim();
            }

                var notes = $('<li class="notesrow">').attr('style', 'list-style:none;padding:10px 0px;')
                            .append('<div><label class="notesdescription"> ' + this.des.trim() + '</label>')
                            .append('<div><ul class="notes-meta-data pull-left"><li>Updated by <a>' + userdisplaytitle + '</a>&nbsp' + this.modifieddate + '</li></ul></div>')
                            .append('<ul class="notes-actions-set pull-right"><li><a class="notesbtupdate" data-noteid=' + this.id + '><i class="glyphicon glyphicon-edit"></i></a></li><li><a class="notes-bt-delete hide "><i class="icon-trash"></i></a></li></ul>')
                            .append('</li>');
                $('.applicantnotes').append(notes)
            });
            }

    $('#applicantnotescontainer').on('hide', function () {
        $('#resetnotesform').click();
    });

    $('#btnaddnewnotes').click(function () {

        $('.top-action-set').addClass('hide');
        $('#notesmodelheader').text('Add New Notes');
        $("#noteslistholder").addClass('hide');
        $('#emptynotesholder').addClass('hide');
        $('#back').addClass('hide');
        $('#newnotesholder').removeClass('hide');
        $('#notesactioncontainer').removeClass('hide');

    });

    $('#notesactioncontainer #cancelnotes').click(function () {
        $('#notesmodelheader').text('Notes');
        $('#btnaddnewnotes').removeClass('hide');

        if ($('#noteslistholder .applicantnotes').find('li').length > 0) {
            $("#noteslistholder").removeClass('hide');
            $('#emptynotesholder').addClass('hide');
        }
        else
        {
            $("#noteslistholder").addClass('hide');
            $('#emptynotesholder').removeClass('hide');
        }
       
        
        $('#newnotesholder').addClass('hide');
        $('#back').removeClass('hide');
        $('#notesactioncontainer').addClass('hide');
        $('.top-action-set').removeClass('hide');
    });

    $('#notesactioncontainer #savenewnotes').click(function () {
        var testcode = '';
        if ($('#newnotescontent').val().trim() == "") {
            $('#attachcandalert').addClass('hide');
            $('#newnotescontent').addClass('alertred');
            $('#newnotescontent').css("cssText", "border:1px solid red !important;width: 98%; padding: 4px 0px 7px 10px !important;");
            testcode += '0';
        }
        else {
            $('#attachcandalert').addClass('hide');
            $('#newnotescontent').removeClass('alertred');
            $('#newnotescontent').css("cssText", "width: 98%; padding: 4px 0px 7px 10px !important;");
            testcode += '1';
                $.ajax({
                    url: "/Jobs/AddNotes",
                    type: "POST",
                    dataType: "JSON",
                    data: {
                        applicationid: $('#currentapplicationid').val(),
                        subjecttype: "applicant",
                        description: $("#newnotescontent ").val()
                    },
                    async: false,
                    success: function (data) {
                        $('.top-action-set').addClass('hide');
                        $('#applicantnotescontainer').modal('hide');
                    },
                    error: function (data) {
                    }

                });
                $('#applicantnotescontainer').modal('hide');
                location.reload();
        }
    });

    $('.updateapplicantnotes').click(function () {
        $('#newnotesholder').removeClass('hide');
        $('#noteslistholder').addClass('hide');
        $('#newnotescontent').val($(this).closest('.notesrow').find('.notesdescription').text());
        $('#currentmodifyingnotesid').val($(this).closest('.notesrow').attr('data-notesid'));
        $('#updapplicantnotes').removeClass('hide');
        $('#btnaddnewnotes').addClass('hide');
    });

    $('.applicantnotes').on('click', '.notesbtupdate', function () {
        $('#newnotesholder').removeClass('hide');
        $('#noteslistholder').addClass('hide');
        $('#emptynotesholder').addClass('hide');
        $(this).data("noteid");
        $('#newnotescontent').val($(this).closest('.notesrow').find('.notesdescription').text());
        $('#updapplicantnotes').removeClass('hide');
        $('#btnaddnewnotes').addClass('forcehide');
        $('#back').addClass('hide');
        $('#updatebuttonnotes').attr('data-noteid', $(this).data("noteid"));
    });

    $('#updatebuttonnotes').click(function () {
        $.ajax({
            url: "/Jobs/UpdateNotes",
            type: "POST",
            dataType: "JSON",
            data: {
                commentid: parseInt($(this).data("noteid")),
                subjectid: $('#currentapplicationid').val(),
                subjecttype: 'applicant',
                objecttype: 'notes',
                Des: $("#newnotescontent").val(),
                visibility: 1,
            },
            async: false,
            success: function (data) {
                $('#applicantnotescontainer').modal('hide');
                $('#btnaddnewnotes').removeClass('forcehide');
            },
            error: function (data) {
            }
        });
        $('#applicantnotescontainer').modal('hide');
        location.reload();
    });

    $('#cancelnotesupdate').click(function () {
        $('#newnotesholder').addClass('hide');
        $('#noteslistholder').removeClass('hide');
        $('#newnotescontent').val("");
        $('#updapplicantnotes').addClass('hide');
        $('#btnaddnewnotes').removeClass('forcehide');
        $('#back').removeClass('hide');
        $('#updatebuttonnotes').attr('data-noteid', "");
    });

    $('.delete-applicant').click(function () {
        $('#chosenapplicantid').val($(this).attr('data-applicantid'));
        $('#applicantname').text($(this).attr('data-name'));
        $('.delete-applicant').parents().closest('.jobrow').addClass('abouttodelete');
        $('#deleteapplicant').modal('show');
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
                if (data.stat == "true") {
                    $('#deleteapplicant').modal('hide');
                    alert("sucessfully deleted");
                    $('.abouttodelete').hide();
                }

            },
            error: function (data) {
            }
        });
        $('#deleteapplicant').modal('hide');
        location.reload();
    });

    $('#deleteapplicantno').click(function () {
        $('#deleteapplicant').modal('hide');
    });
   
    $('.getnotes').click(function () {

            $('#currentqjobid').val($(this).attr('data-jobid'));
            $.ajax({
                url: "/Jobs/GetNotes",
                type: "POST",
                dataType: "JSON",
                data: {

                    parentid: '',
                    applicationid: $('#currentqjobid').val(),
                    objecttype: "notes"
                },
                async: false,
                success: function (data) {
                    $('#jobnotescontainershow').modal('show');
                    if (data.length > 0) {
                        $('#jobnotescontainershow #emptyjobnotesholdershow').addClass('hide');
                        $('#jobnotescontainershow #jobnoteslistholdershow').removeClass('hide');
                        var result = data;
                        notesforjobshow(result);
                    }
                    else {
                        $('#jobnotescontainershow #emptyjobnotesholdershow').removeClass('hide');
                        $('#jobnotescontainershow #jobnoteslistholdershow').addClass('hide');
                    }
                },
                error: function (data) {
                }
            });


        });

    function notesforjobshow(finalresult) {
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

    $('#jobnotescontainershow').on('hide', function () {
            $('#resetnotesjobform').click();
    });

    $('#btnaddnewnotesjobshow').click(function () {

            $('.top-action-set').addClass('hide');
            $('#jobnotesmodelheadershow').text('Add New Notes');
            $("#jobnoteslistholdershow").addClass('hide');
            $('#emptyjobnotesholdershow').addClass('hide')
            $('#back1').addClass('hide');
            $('#jobnewnotesholdershow').removeClass('hide');
            $('#jobnotesactioncontainershow').removeClass('hide');


        });
     
    $('#jobnotesactioncontainershow #jobcancelnotesshow').click(function () {
            $('#jobnotesmodelheadershow').text('Notes');
            $('#btnaddnewnotesshow').removeClass('hide');
            if ($('#jobnoteslistholdershow .jobnotes').find('li').length > 0) {
                $("#jobnoteslistholdershow").removeClass('hide');
                $('#emptyjobnotesholdershow').addClass('hide');
            }
            else {
                $("#jobnoteslistholdershow").addClass('hide');
                $('#emptyjobnotesholdershow').removeClass('hide');
            }
            $('#jobnewnotesholdershow').addClass('hide');
            $('#back1').removeClass('hide');
            $('#jobnotesactioncontainershow').addClass('hide');
            $('.top-action-set').removeClass('hide');
        });

    $('#jobnotesactioncontainershow #jobsavenewnotesshow').click(function () {
            var testcode = '';
            if ($('#jobnewnotescontentshow').val().trim() == "") {
                $('#attachcandalert').addClass('hide');
                $('#jobnewnotescontentshow').addClass('alertred');
                $('#jobnewnotescontentshow').css("cssText", "border:1px solid red !important;width: 98%; padding: 4px 0px 7px 10px !important;");
                testcode += '0';
            }
            else {
                $('#attachcandalert').addClass('hide');
                $('#jobnewnotescontentshow').removeClass('alertred');
                $('#jobnewnotescontentshow').css("cssText", "width: 98%; padding: 4px 0px 7px 10px !important;");
                testcode += '1';
                $.ajax({
                    url: "/Jobs/AddNotes",
                    type: "POST",
                    dataType: "JSON",
                    data: {
                        applicationid: $('#currentqjobid').val(),
                        subjecttype: "job",
                        description: $("#jobnewnotescontentshow ").val()
                    },
                    async: false,
                    success: function (data) {
                        $('.top-action-set').addClass('hide');
                        $('#jobnotescontainershow').modal('hide');
                    },
                    error: function (data) {
                    }

                });
                $('#jobnotescontainershow').modal('hide');
                location.reload();
            }
        });

    $('.jobnotes').on('click', '.notesbtupdatejob', function () {
            $('#jobnewnotesholdershow').removeClass('hide');
            $('#emptyjobnotesholdershow').addClass('hide');
            $('#jobnoteslistholdershow').addClass('hide');
            $(this).data("noteid");
            $('#jobnewnotescontentshow').val($(this).closest('.notesrow').find('.notesdescription').text());
            $('#updjobnotesshow').removeClass('hide');
            $('#btnaddnewnotesjobshow').addClass('forcehide');
            $('#back1').addClass('hide');
            $('#updatebuttonjobnotesshow').attr('data-noteid', $(this).data("noteid"));
        });

    $('#updatebuttonjobnotesshow').click(function () {
            $.ajax({
                url: "/Jobs/UpdateNotes",
                type: "POST",
                dataType: "JSON",
                data: {
                    commentid: parseInt($(this).data("noteid")),
                    subjectid: $('#currentqjobid').val(),
                    subjecttype: 'job',
                    objecttype: 'notes',
                    Des: $("#jobnewnotescontentshow").val(),
                    visibility: 1,
                },
                async: false,
                success: function (data) {
                    $('#jobnotescontainershow').modal('hide');
                    $('#btnaddnewnotesjobshow').removeClass('forcehide');
                },
                error: function (data) {
                }
            });
            $('#jobnotescontainershow').modal('hide');
            location.reload();


        });

    $('#cancelnotesupdateshow').click(function () {
            $('#jobnewnotesholdershow').addClass('hide');
            $('#jobnoteslistholdershow').removeClass('hide');
            $('#jobnewnotescontentshow').val("");
            $('#updjobnotesshow').addClass('hide');
            $('#btnaddnewnotesjobshow').removeClass('forcehide');
            $('#back1').removeClass('hide');
            $('#updatebuttonjobnotesshow').attr('data-noteid', "");
        });

    $('#jobapplicantcollection .chkcand').change(function () {

            var checkedjobs = $('#jobapplicantcollection .chkcand:checked');
            if (checkedjobs.length > 0) {
                $('.action-item').removeClass('disabled');
            }
            else {
                $('.action-item').addClass('disabled');
            }
        });

    $('.detachcandidateinapp').click(function () {
            var jobblock = $('#jobapplicantcollection .chkcand').parents('.jobrow').clone().addClass('abouttodelete');
            var applicantid = $(jobblock).attr('data-applicationid')
            $('#detachcandidatepop').modal('show');

        });

    $('#detachcandidateyes').click(function () {
            var jobblock = $('#jobapplicantcollection .chkcand').parents('.jobrow').clone().addClass('abouttodelete');
            var applicantid = $(jobblock).attr('data-applicationid');
            $.ajax({
                url: "/Jobs/Deleteapplicant",
                type: "POST",
                dataType: "JSON",
                data: {
                    transactionid: applicantid,

                },
                async: false,
                success: function (data) {
                    $('#detachcandidatepop').modal('hide');
                    $('.abouttodelete').hide();

                },
                error: function (data) {
                }
            });
            $('#deleteapplicant').modal('hide');
            location.reload();
        });

    $('#detachcandidateno').click(function () {
            $('#detachcandidatepop').modal('hide');
        });

    $('.forwardto').click(function (e) {
            var checkedjobs = $('#jobapplicantcollection .chkcand:checked');
            $('#candidatelist').empty();
            var i = 0;
            $('#jobapplicantcollection .chkcand:checked').each(function () {
                var jobblock = $(this).parents('.jobrow').clone();
                var urlpath = $(jobblock).attr('data-applicantresume');
                var applicantname = $(jobblock).find('.applicantname');
                var location = $(jobblock).find('.crlocation');
                var relocation = $(jobblock).find('.crrelocation');
                var workauthorization = $(jobblock).find('.crvisa');
                var payrate = $(jobblock).find('.crpayrate'); 
             //   var resume = $(jobblock).find('.resume');
                var jobexperience = $(jobblock).find('.gridLabelValuesexp');
                var jobskills = $(jobblock).find('.gridLabelValuesskills');
                if (i == 0) {
                    var applicant = $('<li>').attr('style', 'list-style:none;padding:10px;width:100%;border-top:1px solid #ccc;margin: auto;')
                    var table = $('<table style="width:900px;margin:auto;">');
                    var contenttr = $('<tr>');
                    $(contenttr).append('<td style="border-top:none;"><div style="padding-right: 10px;word-break: break-all;width: 300px;"><p style="margin: 0 0 3px;"><span style="color:#16a085;font-size:15px;font-weight:bold;text-transform:capitalize;">' + applicantname.text() + '</span >from<span style="text-transform:capitalize;">' + location.text() + '</span></p><span>Relocation : ' + relocation.text() + '</span><br/><span>Work Authorization :' + workauthorization.text() + '</span><br><span>Payrate :<span contenteditable style="min-height:15px;"> ' + payrate.text() + '</span></span><br></div></td>');
                    $(contenttr).append('<td style="border-top:none;"><div style="padding-right: 5px;width: 300px;"><span style="font-weight:bold;color:#857e7e">Availability</span><br></div><div style="padding-right: 10px;width: 300px;"><span>For Interview : <span class="availabilitybox" contenteditable placeholder="Add Comments here" style="min-width: 50px;border: 1px solid #e5e5e5;width: 100px;display: inline-block;min-height:15px;"></span></span><br></div><div style="padding-right: 10px;"><span>To Start : <span class="tostartbox" contenteditable placeholder="Add Comments here" style="min-height:15px;min-width: 50px;border: 1px solid #e5e5e5;width: 100px;display: inline-block;"></span></span><br></div></td>');
                    $(contenttr).append('<td style="border-top:none;"><div style="padding-right: 5px;width: 300px;"><span style="font-weight:bold;color:#857e7e">Contact</span><br></div><div style="padding-right: 10px;width: 300px;"><span>Phone : <span class="phonebox" contenteditable placeholder="Add Comments here" style="min-height:15px;min-width: 50px;border: 1px solid #e5e5e5;width: 100px;display: inline-block;"></span></span><br></div><div style="padding-right: 10px;"><span>Email : <span class="emailbox" contenteditable placeholder="Add Comments here" style="min-height:15px;min-width: 50px;border: 1px solid #e5e5e5;width: 100px;display: inline-block;"></span></span><br></div></td>');
                    $(contenttr).append('<td style="border-top:none;"><div style="width:100px;"><span><a style="float:right;" target="_blank" href="' + urlpath + '"' + '><span style="color:#16a085">View Resume</span></a> </span></div></td></tr>');
                    $(table).append(contenttr);
                    var commenttr = $('<tr >');
                    $(commenttr).append('<td style="border-top:none;" colspan="4"><div><p><span style="margin-right:25px;">Comments:</span><span class="commentbox" contenteditable placeholder="Add Comments here" style="min-width: 50px;min-height: 15px;border: 1px solid #e5e5e5;width: 500px;display: inline-block;"></span></div></td>');
                    $(table).append(commenttr);
                    $(applicant).append(table);
                    $('#candidatelist').append(applicant);
                }
                else {
                  
                }

                var loopjobid = $(jobblock).attr('data-jobid');
                if ($('#applicantcollectionstring').val() == "") {
                    $('#applicantscollectionstring').val(loopjobid);
                }
                else {
                    $('#applicantcollectionstring').val($('#applicantcollectionstring').val() + ';' + loopjobid)
                }
            })
            if ($('#jobapplicantcollection .chkcand:checked').length > 0) {
                $('#forwardtoclient').modal('show');
            }

           // $('#mailmessagesubject').val('Company Has Shared Jobs With You');
            var mailcontent = $('#mailbodyholder').clone();
            $(mailcontent).find('.emailpreviewline').remove();
            $('#mailmessagebody').val($(mailcontent).html());
        });

});
  


