﻿//filtros
$(document).ready(function () {
    $("#filtro").on("keyup", function () {
        var filtroTexto = $("#filtro").val().toLowerCase();
        var nenhumEventoEncontrado = true;


        $(".evento").each(function () {
            var eventoTexto = $(this).text().toLowerCase();

            if (eventoTexto.indexOf(filtroTexto) === -1) {
                $(this).hide();
            } else {
                $(this).show();
                nenhumEventoEncontrado = false;
            }
        });

        if (nenhumEventoEncontrado) {
            $("#nenhum-evento-encontrado").show();
        } else {
            $("#nenhum-evento-encontrado").hide();
        }
    });
});

$(document).ready(function () {
    $("#filtro-noticias").on("keyup", function () {
        var filtroTexto = $("#filtro-noticias").val().toLowerCase();
        var nenhumaNoticiaEncontrada = true;


        $(".noticia").each(function () {
            var eventoTexto = $(this).text().toLowerCase();

            if (eventoTexto.indexOf(filtroTexto) === -1) {
                $(this).hide();
            } else {
                $(this).show();
                nenhumaNoticiaEncontrada = false;
            }
        });

        if (nenhumaNoticiaEncontrada) {
            $("#nenhuma-noticia-encontrada").show();
        } else {
            $("#nenhuma-noticia-encontrada").hide();
        }
    });
});


$(".close-alert").click(function () {
    $('.alert').hide('hide');
});