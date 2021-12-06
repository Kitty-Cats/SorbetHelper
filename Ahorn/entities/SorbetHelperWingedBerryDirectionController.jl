module SorbetHelperWingedBerryDirectionController

using ..Ahorn, Maple

@mapdef Entity "SorbetHelper/WingedBerryDirectionController" Controller(
    x::Int,
    y::Int,
)

const placements = Ahorn.PlacementDict(
    "Winged Strawberry Direction Controller (Sorbet Helper)" => Ahorn.EntityPlacement(
        Controller,
    )
)

const sprite = "objects/SorbetHelper/wingedBerryDirectionController/icon"

function Ahorn.render(ctx::Ahorn.Cairo.CairoContext, entity::Controller)
    Ahorn.drawSprite(ctx, sprite, 0, 0)
end

function Ahorn.selection(entity::Controller)
    x, y = Ahorn.position(entity)
    return Ahorn.Rectangle(x - 8, y - 11, 22, 22)
end

end
