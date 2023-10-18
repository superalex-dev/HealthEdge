<template>
  <div>
    <NavBar />
    <HeroBanner />
    <FeatureCard icon="HealthEdge.png" title="Feature" description="Feature description" />
    <!-- ... other FeatureCards ... -->
  </div>
</template>

<script>
import NavBar from "@/components/NavBar.vue";
//import HeroBanner from "@/components/HeroBanner.vue";
//import FeatureCard from "@/components/FeatureCard.vue";

export default {
  components: {
    NavBar,
    //HeroBanner,
    //FeatureCard
  }
}
</script>