<script>
    function getAvailableTrucks() {
            var weight = $("#Weight").val();

    $.ajax({
        url: "/Order/GetAvailableTrucks",
    type: "GET",
    data: {weight: weight },
    success: function (data) {
        $("#availableTrucks").html(data);
                },
    error: function () {
        alert("Произошла ошибка при загрузке доступных грузовиков.");
                }
            });
        }
</script>