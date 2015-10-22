Shader "Custom/fogOfWar_Shader" {
	Properties{
		_Point("Camera Point", Vector) = (0., 0., 0., 1.0)
		_PlayerPoint("Player Point", Vector) = (0., 0., 0., 1.0)
		_DistanceNear("Camera Size", Float) = 5.0
		_DistancePlayer("Player Size", Float) = 5.0
	}
	SubShader{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		 

		Pass{
			Cull Off // first pass renders only back faces 
			ZWrite Off // don't write to depth buffer 
					   // in order not to occlude other objects
			
			Blend SrcAlpha OneMinusSrcAlpha //alpha blending
			CGPROGRAM

			#pragma vertex vert 
			#pragma fragment frag

			uniform float4 _Point;
			uniform float _DistanceNear;
			uniform float4 _PlayerPoint;
			uniform float _DistancePlayer;

			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 col : TEXCOORD0;
			};

			vertexOutput vert(float4 vertexPos : POSITION)
			{
				vertexOutput output;

				output.pos = mul(UNITY_MATRIX_MVP, vertexPos);
				output.col = mul(_Object2World, vertexPos);
				return output;
			}

			struct FragOut
			{
				float4 color : COLOR;
			};

			float4 frag(vertexOutput input) : COLOR
			{
				float dist = distance(input.col, _Point);
				float distPlayer = distance(input.col, _PlayerPoint);

				if (dist < _DistanceNear) {
					return float4(0, 0, 0, 0); //Show FOV of camera
				} else if (distPlayer < _DistancePlayer) {
					return float4(0, 0, 0, 0); //Show player position
				} else if (distPlayer > _DistancePlayer) { //Player blur
					float diff = distPlayer - _DistancePlayer;
			/*
					if (_DistancePlayer < 3) {
						diff = diff * 4;
					}
			*/	
					return float4(0, 0, 0, clamp(0 + diff, 0.0, 0.98));
				} else { //Camera blur
					float diff = dist - _DistanceNear;
			/*
					if (_DistanceNear < 3) {
						diff = diff * 4;
					}
			*/
					return float4(0, 0, 0, clamp(0 + diff, 0.0, 0.98));
				}
			}

		ENDCG
		}
		
	}
}
