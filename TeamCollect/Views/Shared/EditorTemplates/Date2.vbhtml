@ModelType String
<div class="input-group input-icon">
    <span class="input-group-addon"><i class="fa fa-calendar s16"></i></span>
    @Html.TextBoxFor(Function(m) m, New With {.class = "form-control", .id = "mask-date", .type = "text"})
</div>


