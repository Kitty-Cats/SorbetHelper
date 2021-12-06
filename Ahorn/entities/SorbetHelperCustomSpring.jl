module SorbetHelperCustomSpring

using ..Ahorn, Maple

@mapdef Entity "SorbetHelper/CustomSpringUp" CustomSpring(x::Integer, y::Integer, playerCanUse::Bool=true, width::Integer=16)
@mapdef Entity "SorbetHelper/CustomSpringRight" CustomSpringRight(x::Integer, y::Integer, playerCanUse::Bool=true, height::Integer=16)
@mapdef Entity "SorbetHelper/CustomSpringLeft" CustomSpringLeft(x::Integer, y::Integer, playerCanUse::Bool=true, height::Integer=16)

const placements = Ahorn.PlacementDict(
    "Custom Spring (Up) (Sorbet Helper)" => Ahorn.EntityPlacement(
        CustomSpring,
        "rectangle"
    ),
    "Custom Spring (Left) (Sorbet Helper)" => Ahorn.EntityPlacement(
        CustomSpringRight,
        "rectangle"
    ),
    "Custom Spring (Right) (Sorbet Helper)" => Ahorn.EntityPlacement(
        CustomSpringLeft,
        "rectangle"
    )
)

Ahorn.resizable(entity::CustomSpring) = true, false
Ahorn.resizable(entity::CustomSpringLeft) = false, true
Ahorn.resizable(entity::CustomSpringRight) = false, true

Ahorn.minimumSize(entity::CustomSpring) = 16, 0
Ahorn.minimumSize(entity::CustomSpringLeft) = 0, 16
Ahorn.minimumSize(entity::CustomSpringRight) = 0, 16

function Ahorn.selection(entity::CustomSpring)
    x, y = Ahorn.position(entity)

    return Ahorn.Rectangle(x - 6, y - 3, 12, 5)
end

function Ahorn.selection(entity::CustomSpringLeft)
    x, y = Ahorn.position(entity)

    return Ahorn.Rectangle(x - 1, y - 6, 5, 12)
end

function Ahorn.selection(entity::CustomSpringRight)
    x, y = Ahorn.position(entity)

    return Ahorn.Rectangle(x - 4, y - 6, 5, 12)
end

sprite = "objects/spring/00.png"

Ahorn.render(ctx::Ahorn.Cairo.CairoContext, entity::CustomSpring, room::Maple.Room) = Ahorn.drawSprite(ctx, sprite, 0, -8)
Ahorn.render(ctx::Ahorn.Cairo.CairoContext, entity::CustomSpringLeft, room::Maple.Room) = Ahorn.drawSprite(ctx, sprite, 9, -11, rot=pi / 2)
Ahorn.render(ctx::Ahorn.Cairo.CairoContext, entity::CustomSpringRight, room::Maple.Room) = Ahorn.drawSprite(ctx, sprite, 3, 1, rot=-pi / 2)

function Ahorn.flipped(entity::CustomSpringLeft, horizontal::Bool)
    if horizontal
        return CustomSpringRight(entity.x, entity.y)
    end
end

function Ahorn.flipped(entity::CustomSpringRight, horizontal::Bool)
    if horizontal
        return CustomSpringLeft(entity.x, entity.y)
    end
end

end