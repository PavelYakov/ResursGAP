<script>
    $(document).ready(function () {
        $('[data-confirm]').click(function () {
            return confirm($(this).data('confirm'));
        });
    });
</script>